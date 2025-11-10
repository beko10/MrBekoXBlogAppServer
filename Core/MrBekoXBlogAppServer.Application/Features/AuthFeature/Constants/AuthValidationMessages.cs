namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.Constants;

public static class AuthValidationMessages
{
    // Common Validation Messages
    public const string IsRequired = "Bu alan zorunludur.";
    public const string InvalidFormat = "Geçersiz format.";

    // Login Command Messages
    public static class Login
    {
        public static class Email
        {
            public const string NotEmpty = "Email alanı zorunludur.";
            public const string InvalidFormat = "Geçerli bir email adresi giriniz.";
            public const string MaxLength = "Email en fazla 256 karakter olabilir.";
        }

        public static class Password
        {
            public const string NotEmpty = "Şifre alanı zorunludur.";
            public const string MinLength = "Şifre en az 8 karakter olmalıdır.";
            public const string MaxLength = "Şifre en fazla 128 karakter olabilir.";
        }

        public static class DeviceInfo
        {
            public const string MaxLength = "Cihaz bilgisi en fazla 500 karakter olabilir.";
        }

        public static class IpAddress
        {
            public const string InvalidFormat = "Geçerli bir IP adresi formatı giriniz (IPv4 veya IPv6).";
        }
    }

    // Logout Command Messages
    public static class Logout
    {
        public static class RefreshToken
        {
            public const string NotEmpty = "Refresh token alanı zorunludur.";
            public const string MinLength = "Refresh token en az 32 karakter olmalıdır.";
            public const string MaxLength = "Refresh token en fazla 500 karakter olabilir.";
        }

        public static class Jti
        {
            public const string InvalidFormat = "Geçersiz JWT ID formatı (GUID formatında olmalıdır).";
        }
    }

    // RefreshToken Command Messages
    public static class RefreshToken
    {
        public static class Token
        {
            public const string NotEmpty = "Refresh token alanı zorunludur.";
            public const string MinLength = "Refresh token en az 32 karakter olmalıdır.";
            public const string MaxLength = "Refresh token en fazla 500 karakter olabilir.";
        }
    }

    // Register Command Messages
    public static class Register
    {
        public static class Email
        {
            public const string NotEmpty = "Email alanı zorunludur.";
            public const string InvalidFormat = "Geçerli bir email adresi giriniz.";
            public const string MaxLength = "Email en fazla 256 karakter olabilir.";
        }

        public static class Password
        {
            public const string NotEmpty = "Şifre alanı zorunludur.";
            public const string MinLength = "Şifre en az 8 karakter olmalıdır.";
            public const string MustContainUppercase = "Şifre en az bir büyük harf içermelidir.";
            public const string MustContainLowercase = "Şifre en az bir küçük harf içermelidir.";
            public const string MustContainDigit = "Şifre en az bir rakam içermelidir.";
            public const string MustContainSpecialChar = "Şifre en az bir özel karakter içermelidir.";
        }

        public static class FullName
        {
            public const string NotEmpty = "Ad Soyad alanı zorunludur.";
            public const string MinLength = "Ad Soyad en az 2 karakter olmalıdır.";
            public const string MaxLength = "Ad Soyad en fazla 100 karakter olabilir.";
        }

        public static class PhoneNumber
        {
            public const string InvalidFormat = "Geçerli bir telefon numarası formatı giriniz.";
        }
    }

    // Query Messages
    public static class Query
    {
        public const string Unauthorized = "Yetkisiz erişim. Lütfen giriş yapın.";
        public const string UserIdRequired = "Kullanıcı ID'si zorunludur.";
        public const string InvalidUserId = "Geçersiz kullanıcı ID formatı.";
    }
}

