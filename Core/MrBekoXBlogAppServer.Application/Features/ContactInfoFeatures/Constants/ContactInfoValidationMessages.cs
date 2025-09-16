namespace MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.Constants;

public static class ContactInfoValidationMessages
{
    // Common Validation Messages
    public const string IsRequired = "Bu alan zorunludur.";
    public const string IdMustBeValidGuid = "Geçerli bir ID formatı giriniz.";

    // GetById Specific Messages
    public static class GetById
    {
        public const string IdRequired = "İletişim bilgisi ID'si zorunludur.";
        public const string IdMustBeValidGuid = "Geçerli bir İletişim bilgisi ID'si giriniz.";
    }

    // Create Specific Messages
    public static class Create
    {
        public static class Address
        {
            public const string NotEmpty = "Adres alanı boş olamaz.";
            public const string MinLength = "Adres en az 10 karakter olmalıdır.";
            public const string MaxLength = "Adres en fazla 500 karakter olabilir.";
        }

        public static class Email
        {
            public const string NotEmpty = "Email alanı boş olamaz.";
            public const string InvalidFormat = "Geçerli bir email adresi giriniz.";
            public const string MaxLength = "Email en fazla 100 karakter olabilir.";
        }

        public static class Phone
        {
            public const string NotEmpty = "Telefon alanı boş olamaz.";
            public const string MinLength = "Telefon numarası en az 10 karakter olmalıdır.";
            public const string MaxLength = "Telefon numarası en fazla 15 karakter olabilir.";
            public const string InvalidFormat = "Geçerli bir telefon numarası giriniz.";
        }

        public static class MapUrl
        {
            public const string NotEmpty = "Harita URL'i boş olamaz.";
            public const string InvalidFormat = "Geçerli bir URL giriniz.";
            public const string MaxLength = "Harita URL'i en fazla 1000 karakter olabilir.";
        }
    }

    // Update Specific Messages
    public static class Update
    {
        public const string IdRequired = "Güncellenecek iletişim bilgisi ID'si zorunludur.";
        public const string IdMustBeValidGuid = "Geçerli bir güncellenecek iletişim bilgisi ID'si giriniz.";

        public static class Address
        {
            public const string NotEmpty = "Adres alanı boş olamaz.";
            public const string MinLength = "Adres en az 10 karakter olmalıdır.";
            public const string MaxLength = "Adres en fazla 500 karakter olabilir.";
        }

        public static class Email
        {
            public const string NotEmpty = "Email alanı boş olamaz.";
            public const string InvalidFormat = "Geçerli bir email adresi giriniz.";
            public const string MaxLength = "Email en fazla 100 karakter olabilir.";
        }

        public static class Phone
        {
            public const string NotEmpty = "Telefon alanı boş olamaz.";
            public const string MinLength = "Telefon numarası en az 10 karakter olmalıdır.";
            public const string MaxLength = "Telefon numarası en fazla 15 karakter olabilir.";
            public const string InvalidFormat = "Geçerli bir telefon numarası giriniz.";
        }

        public static class MapUrl
        {
            public const string NotEmpty = "Harita URL'i boş olamaz.";
            public const string InvalidFormat = "Geçerli bir URL giriniz.";
            public const string MaxLength = "Harita URL'i en fazla 1000 karakter olabilir.";
        }
    }

    // Delete Specific Messages
    public static class Delete
    {
        public const string IdRequired = "Silinecek iletişim bilgisi ID'si zorunludur.";
        public const string IdMustBeValidGuid = "Geçerli bir silinecek iletişim bilgisi ID'si giriniz.";
    }

    // GetAll Specific Messages
    public static class GetAll
    {
        public const string PageSizeMustBePositive = "Sayfa boyutu pozitif bir sayı olmalıdır.";
        public const string PageNumberMustBePositive = "Sayfa numarası pozitif bir sayı olmalıdır.";
        public const string MaxPageSize = "Sayfa boyutu en fazla 100 olabilir.";
    }
}
