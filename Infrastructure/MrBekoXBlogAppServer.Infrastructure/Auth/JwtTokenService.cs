using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MrBekoXBlogAppServer.Application.Common.DTOs.Security;
using MrBekoXBlogAppServer.Application.Interfaces.Auth;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.RefreshTokenRepository;
using MrBekoXBlogAppServer.Application.Interfaces.UnitOfWorks;
using MrBekoXBlogAppServer.Domain.Entities;
using MrBekoXBlogAppServer.Infrastructure.Auth.Options;
using MrBekoXBlogAppServer.Infrastructure.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace MrBekoXBlogAppServer.Infrastructure.Auth; 


public sealed class JwtTokenService(
    // appsettings.json'daki JWT ayarlarını almak için kullanılır.
    IOptions<JwtOptions> options,
    // Kullanıcıları yönetmek (bulmak, rollerini almak vb.) için Identity kütüphanesi.
    UserManager<AppUser> userManager,
    // Refresh token'ları okumak için repository.
    IRefreshTokenReadRepository refreshTokenReadRepository,
    // Refresh token'ları yazmak/güncellemek için repository.
    IRefreshTokenWriteRepository refreshTokenWriteRepository,
    // Veritabanı işlemlerini tek bir transaction olarak yönetmek için.
    IUnitOfWork unitOfWork
// IJwtTokenService arayüzünü implemente eder.
) : IJwtTokenService
{
    // Enjekte edilen IOptions'tan gelen JWT ayarlarını sınıf içinde kullanmak üzere bir alana atar.
    private readonly JwtOptions _options = options.Value;

    // Bu politika, yalnızca kullanıcı giriş yaptığında (login) eski token'ların iptal edilip edilmeyeceğini belirler.
    // Refresh işlemi sırasında bu politika uygulanmaz.
    private const bool RevokeExistingRefreshTokensOnLogin = true;

    // === ORTAK TOKEN ÜRETİMİ (login & refresh) ===
    // Bu özel (private) metot, hem login hem de refresh işlemlerinde token çifti (access + refresh) üretmek için kullanılır.
    // Kod tekrarını önler.
    

    // === LOGIN: Access + Refresh üretir ===
    // Kullanıcı girişi (login) başarılı olduğunda bu metot çağrılır.
    public async Task<TokenDto> CreateAccessTokenAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        // Verilen ID ile kullanıcıyı veritabanında bulur.
        var user = await userManager.FindByIdAsync(userId);
        // Kullanıcı bulunamazsa bir istisna fırlatır.
        if (user is null) throw new InvalidOperationException("User not found.");

        // Ortak token üretme metodunu çağırır.
        // 'revokeAllExistingRefreshTokens' parametresine `true` geçerek, bu login işleminde kullanıcının eski tüm oturumlarının sonlandırılmasını sağlar.
        return await GenerateTokenPairInternalAsync(
            user,
            revokeAllExistingRefreshTokens: RevokeExistingRefreshTokensOnLogin,
            cancellationToken);
    }

    // === REFRESH: Rotasyon + reuse detection ===
    // Access token süresi dolduğunda, client bu metodu çağırarak yeni bir token çifti alır.
    public async Task<TokenDto> RefreshAsync(
        // Client'tan gelen ham refresh token.
        string refreshTokenRaw,
        CancellationToken cancellationToken = default)
    {
        // Şu anki UTC zamanını alır.
        var now = DateTime.UtcNow;
        // Gelen ham token'ın hash'ini hesaplar. Veritabanında bu hash ile arama yapılacak.
        var usedHash = TokenHasher.HashToken(refreshTokenRaw);

        // Bu bloktaki tüm veritabanı işlemlerinin atomik (hepsi ya başarılı ya da hepsi başarısız) olmasını sağlamak için
        // bir veritabanı transaction'ı başlatılabilir (opsiyonel).
        // await using var tx = await unitOfWork.BeginTransactionAsync(cancellationToken);

        // 1) Kullanılan refresh token'ı hash'i üzerinden veritabanında bulur.
        // 'tracking: true' olarak ayarlanır çünkü bu entity'yi daha sonra güncelleyeceğiz (iptal edeceğiz).
        var used = await refreshTokenReadRepository.GetSingleAsync(
            x => x.TokenHash == usedHash,
            tracking: true,
            autoInclude: true,
            cancellationToken
        );

        // 2) Temel doğrulamalar
        // Token veritabanında bulunamazsa, geçersiz bir token demektir. Yetkisiz erişim hatası fırlatılır.
        if (used is null)
            throw new UnauthorizedAccessException("Invalid refresh token.");

        // Eğer token'ın 'RevokedAt' alanı doluysa, bu token daha önce iptal edilmiş demektir.
        if (used.RevokedAt != null)
        {
            // YENİDEN KULLANIM TESPİTİ (REUSE DETECTION): Bu, bir güvenlik ihlali sinyalidir. Çalınan bir token tekrar kullanılmaya çalışılıyor olabilir.
            // Bu durumda, güvenlik önlemi olarak bu kullanıcıya ait TÜM refresh token'ları iptal ederiz.
            await refreshTokenWriteRepository.RevokeAllAsync(used.UserId, "Refresh token reuse detected", cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            // Transaction'ı onayla.
            // if (tx is not null) await tx.CommitAsync(cancellationToken); 
            // İstemciye token'ın yeniden kullanıldığını belirten bir hata fırlatırız.
            throw new UnauthorizedAccessException("Token reuse detected.");
        }

        // Token'ın süresi dolmuşsa, hata fırlatılır.
        if (used.ExpiresAt <= now)
            throw new UnauthorizedAccessException("Refresh token expired.");

        // 3) Token'a ait kullanıcıyı buluruz.
        var user = await userManager.FindByIdAsync(used.UserId);
        // Kullanıcı artık sistemde mevcut değilse (silinmişse), hata fırlatılır.
        if (user is null)
            throw new UnauthorizedAccessException("User no longer exists.");

        // 4) Yeni bir token çifti (access + refresh) üretiriz.
        // Bu sefer 'revokeAllExistingRefreshTokens' parametresine `false` geçilir çünkü bu bir refresh işlemidir,
        // kullanıcının diğer cihazlardaki oturumlarını kapatmak istemeyiz.
        var pair = await GenerateTokenPairInternalAsync(
            user,
            revokeAllExistingRefreshTokens: false,
            cancellationToken);

        // 5) ROTASYON: Eski (az önce kullanılan) token'ı geçersiz kılarız.
        // İptal zamanını şu an olarak ayarla.
        used.RevokedAt = now;

        // İptal nedenini "rote edildi" olarak belirt.
        used.RevokedReason = "Rotated";

        // Eski token'ı, yerine geçen yeni token'a bağlarız. Bu, token ailesini takip etmeyi sağlar.
        used.ReplacedByTokenHash = TokenHasher.HashToken(pair.RefreshToken.Token);

        // Eski token'daki bu değişiklikleri veritabanına kaydederiz.
        await unitOfWork.SaveChangesAsync(cancellationToken);
        // Transaction'ı onayla.
        // if (tx is not null) await tx.CommitAsync(cancellationToken); 

        // Yeni oluşturulan token çiftini istemciye döndürürüz.
        return pair;
    }

    //=== ORTAK TOKEN ÜRETİMİ (login & refresh) ===
    // Hem login hem de refresh işlemlerinde token çifti (access + refresh) üretir.
    private async Task<TokenDto> GenerateTokenPairInternalAsync(
        // Token'ları üretilecek olan kullanıcı.
        AppUser user,
        // Kullanıcının mevcut tüm refresh token'ları iptal edilsin mi?
        bool revokeAllExistingRefreshTokens,
        CancellationToken cancellationToken)
    {
        // Tüm zaman damgaları için sunucu saatini UTC (Evrensel Zaman) olarak alır. Bu, zaman dilimi sorunlarını önler.
        var now = DateTime.UtcNow;

        // Access token'ın son geçerlilik tarihini ayarlar'dan okunan dakikaya göre hesaplar.
        var accessTokenExpiresAt = now.AddMinutes(_options.AccessTokenExpirationMinutes);

        // Refresh token'ın son geçerlilik tarihini ayarlar'dan okunan güne göre hesaplar.
        var refreshTokenExpiresAt = now.AddDays(_options.RefreshTokenExpirationDays);

        // JWT'yi imzalamak için kullanılacak olan gizli anahtarı (secret key) byte dizisine çevirir.
        var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_options.SecretKey));

        // İmzalama algoritması olarak HMAC-SHA256 kullanılacağını ve anahtarın ne olduğunu belirten kimlik bilgileri oluşturur.
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Her access token için benzersiz bir kimlik (JWT ID) oluşturur. Bu, token'ı daha sonra izlemek veya iptal etmek için kullanılabilir.
        var jti = Guid.NewGuid().ToString();

        // Kullanıcıya özel talepleri (claims) (rol, isim, e-posta vb.) almak için yardımcı metodu çağırır.
        var baseClaims = await GetClaims(user, _options.Audience, cancellationToken);

        // JWT'nin standart taleplerini (sub, jti, iat) kullanıcı talepleriyle birleştirir.
        // Token'ın üretildiği zamanı Unix epoch formatına çevirir.
        var nowEpoch = EpochTime.GetIntDate(now).ToString();
        var allClaims = baseClaims.Concat(new[]
        {
            // 'subject', yani token'ın sahibi olan kullanıcının ID'si.
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),

            // 'JWT ID', token'ın benzersiz kimliği.
            new Claim(JwtRegisteredClaimNames.Jti, jti),

            // 'issued at', token'ın üretildiği zaman.
            new Claim(JwtRegisteredClaimNames.Iat, nowEpoch, ClaimValueTypes.Integer64)
        });

        // JWT token nesnesini oluşturur.
        var jwt = new JwtSecurityToken(

            // Token'ı yayınlayan (bizim sunucumuz).
            issuer: _options.Issuer,

            // Token içinde taşınacak tüm talepler.
            claims: allClaims,

            // Token'ın bu zamandan önce geçerli olmadığını belirtir.
            notBefore: now,

            // Token'ın son geçerlilik tarihi.
            expires: accessTokenExpiresAt,

            // Token'ı imzalamak için kullanılacak kimlik bilgileri.
            signingCredentials: signingCredentials);

        // JWT token nesnesini okunabilir bir string formatına (Bearer token) dönüştürür.
        var handler = new JwtSecurityTokenHandler();
        var accessTokenString = handler.WriteToken(jwt);

        // Eğer 'revokeAllExistingRefreshTokens' parametresi true ise (yani bu bir login işlemiyse),
        // kullanıcının veritabanındaki tüm aktif refresh token'larını iptal eder.
        if (revokeAllExistingRefreshTokens)
        {
            await refreshTokenWriteRepository.RevokeAllAsync(user.Id, "New login", cancellationToken);
        }

        // Client'a gönderilecek olan yeni, ham (raw) refresh token'ı üretir.
        var refreshTokenRaw = GenerateRefreshToken();

        // Veritabanında saklanacak olan refresh token'ın hash'ini alır. (Güvenlik için ham token asla DB'ye yazılmaz).
        var refreshTokenHash = TokenHasher.HashToken(refreshTokenRaw);

        // Veritabanına kaydedilecek yeni RefreshToken entity'sini oluşturur.
        var refreshTokenEntity = new RefreshToken
        {
            UserId = user.Id,
            // Sadece hash'i saklanır.
            TokenHash = refreshTokenHash,
            ExpiresAt = refreshTokenExpiresAt,

            // Henüz başka bir token ile değiştirilmedi.
            ReplacedByTokenHash = null,

            // Henüz iptal edilmedi.
            RevokedAt = null,
            RevokedReason = null
        };

        // Yeni refresh token'ı veritabanına eklemek üzere EF Core'un Change Tracker'ına ekler.
        await refreshTokenWriteRepository.AddAsync(refreshTokenEntity, cancellationToken);

        // Tüm değişiklikleri (eski token'ların iptali, yeni token'ın eklenmesi) tek bir işlemde veritabanına kaydeder.
        await unitOfWork.SaveChangesAsync(cancellationToken);

        // API yanıtı olarak istemciye gönderilecek olan DTO'yu oluşturur.
        return new TokenDto
        {
            AccessToken = new JwtAccessTokenDto
            {
                // Oluşturulan Bearer token.
                Token = accessTokenString,
                // Son geçerlilik tarihi.
                ExpirationAt = accessTokenExpiresAt,
                // Benzersiz token kimliği.
                Jti = jti
            },
            RefreshToken = new RefreshTokenDto
            {
                // Ham refresh token (sadece client bilir).
                Token = refreshTokenRaw,
                // Refresh token'ın son geçerlilik tarihi.
                ExpiresAt = refreshTokenExpiresAt
            }
        };
    }

    // URL'lerde güvenle kullanılabilecek, kriptografik olarak güçlü bir refresh token üretir.
    private static string GenerateRefreshToken()
    {
        // 256 bitlik bir güvenlik seviyesi için 32 byte'lık rastgele veri oluştur.
        var bytes = new byte[32];

        // Bu byte dizisini kriptografik olarak güvenli rastgele sayılarla doldur.
        RandomNumberGenerator.Fill(bytes);

        // Byte dizisini URL-safe bir Base64 string'ine dönüştür.
        return Base64UrlEncoder.Encode(bytes);
    }

    // Verilen kullanıcı için JWT'ye eklenecek talepleri (claims) oluşturur.
    private async Task<IEnumerable<Claim>> GetClaims(
        AppUser user,
        List<string> audience,
        CancellationToken cancellationToken)
    {
        // Kullanıcının rollerini asenkron olarak alır.
        var roles = await userManager.GetRolesAsync(user);

        // Talepler listesini başlatır.
        var claims = new List<Claim>
        {
            // Standard claim: Kullanıcı ID'si.
            new Claim(ClaimTypes.NameIdentifier, user.Id),

            // Standard claim: Kullanıcının tam adı.
            new Claim(ClaimTypes.Name, user.FullName ?? string.Empty),
        };

        // Kullanıcının e-postası varsa, bunu da bir claim olarak ekler.
        if (!string.IsNullOrWhiteSpace(user.Email))
            claims.Add(new Claim(ClaimTypes.Email, user.Email!));

        // Kullanıcının her bir rolünü ayrı bir 'Role' claim'i olarak ekler.
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        // Token'ın geçerli olduğu kitleleri (audience) 'aud' claim'leri olarak ekler.
        claims.AddRange(audience.Select(aud => new Claim(JwtRegisteredClaimNames.Aud, aud)));

        // Oluşturulan talep listesini döndürür.
        return claims;
    }
}