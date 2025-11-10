namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.Constants;

/// <summary>
/// Kullanıcıya gösterilecek Auth business rule mesajları
/// </summary>
public static class AuthBusinessRuleMessages
{
    #region Login Rules

    public const string UserNotFound = "Kullanıcı bulunamadı.";
    public const string UserNotActive = "Kullanıcı hesabı aktif değil.";
    public const string EmailNotConfirmed = "Email adresi doğrulanmamış. Lütfen email adresinizi doğrulayın.";
    public const string UserLockedOut = "Hesabınız geçici olarak kilitlenmiştir. Lütfen daha sonra tekrar deneyin.";
    public const string InvalidPassword = "Şifre hatalı.";
    public const string TooManyLoginAttempts = "Çok fazla başarısız giriş denemesi. Lütfen bir süre bekleyip tekrar deneyin.";

    #endregion

    #region Register Rules

    public const string EmailAlreadyExists = "Bu email adresi zaten kullanımda.";
    public const string UsernameAlreadyExists = "Bu kullanıcı adı zaten kullanımda.";
    public const string InvalidEmailFormat = "Geçersiz email formatı.";
    public const string WeakPassword = "Şifre güvenlik gereksinimlerini karşılamıyor.";
    public const string UserCountLimitExceeded = "Maksimum kullanıcı sayısına ulaşıldı.";
    public const string InvalidAge = "Yaş gereksinimlerini karşılamıyorsunuz.";

    #endregion

    #region RefreshToken Rules

    public const string InvalidRefreshToken = "Geçersiz refresh token.";
    public const string RefreshTokenExpired = "Refresh token süresi dolmuş.";
    public const string RefreshTokenRevoked = "Refresh token iptal edilmiş.";
    public const string UserNoLongerExists = "Kullanıcı artık mevcut değil.";
    public const string UserNoLongerActive = "Kullanıcı hesabı artık aktif değil.";
    public const string TokenReuseDetected = "Token yeniden kullanım tespit edildi. Güvenlik nedeniyle tüm oturumlar sonlandırıldı.";

    #endregion

    #region Logout Rules

    public const string UserMustBeAuthenticated = "Kullanıcı doğrulanmış olmalıdır.";
    public const string InvalidToken = "Geçersiz token.";

    #endregion

    #region Common Rules

    public const string UserMustExist = "Kullanıcı mevcut olmalıdır.";
    public const string UserMustBeActive = "Kullanıcı hesabı aktif olmalıdır.";

    #endregion
}
