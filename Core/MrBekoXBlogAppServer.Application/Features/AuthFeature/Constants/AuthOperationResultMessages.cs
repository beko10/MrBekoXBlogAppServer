namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.Constants;

public static class AuthOperationResultMessages
{
    #region Başarılı Mesajlar

    // Login
    public const string LoginSuccess = "Giriş başarıyla tamamlandı.";

    // Logout
    public const string LogoutSuccess = "Çıkış başarıyla tamamlandı.";

    // Register
    public const string RegisterSuccess = "Kayıt başarıyla tamamlandı.";

    // Refresh Token
    public const string RefreshTokenSuccess = "Token başarıyla yenilendi.";

    // Get Operations
    public const string GetUserProfileSuccess = "Kullanıcı profili başarıyla getirildi.";
    public const string GetActiveSessionsSuccess = "Aktif oturumlar başarıyla getirildi.";

    #endregion

    #region Hata Mesajları

    // Authentication Failures
    public const string LoginFailed = "Giriş başarısız. Email veya şifre hatalı.";
    public const string InvalidEmailOrPassword = "Geçersiz email veya şifre.";
    public const string LogoutFailed = "Çıkış işlemi başarısız oldu.";
    public const string RegisterFailed = "Kayıt işlemi başarısız oldu.";
    public const string UserCreationFailed = "Kullanıcı oluşturma başarısız oldu.";
    public const string RefreshTokenFailed = "Token yenileme işlemi başarısız oldu.";
    public const string InvalidRefreshToken = "Geçersiz refresh token.";
    public const string TokenExpired = "Token süresi dolmuş.";
    public const string TokenReuseDetected = "Token yeniden kullanım tespit edildi. Güvenlik nedeniyle tüm oturumlar sonlandırıldı.";

    // Not Found
    public const string UserNotFound = "Kullanıcı bulunamadı.";
    public const string SessionNotFound = "Oturum bulunamadı.";
    public const string NoActiveSessions = "Aktif oturum bulunamadı.";

    // Get Operations Failures
    public const string GetUserProfileFailed = "Kullanıcı profili getirilemedi.";
    public const string GetActiveSessionsFailed = "Aktif oturumlar getirilemedi.";

    // General Failures
    public const string OperationFailed = "İşlem başarısız oldu.";
    public const string Unauthorized = "Yetkisiz erişim.";

    #endregion
}

