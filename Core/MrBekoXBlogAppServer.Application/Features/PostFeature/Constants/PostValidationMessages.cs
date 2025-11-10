namespace MrBekoXBlogAppServer.Application.Features.PostFeature.Constants;

public class PostValidationMessages
{
    public const string PostIdRequired = "Yazı Id zorunludur.";
    public const string PostIdMustBeValidGuid = "Yazı Id geçerli bir GUID olmalıdır.";
    public const string TitleRequired = "Yazı başlığı zorunludur.";
    public const string TitleMaxLength = "Yazı başlığı 200 karakterden uzun olamaz.";
    public const string TitleMinLength = "Yazı başlığı en az 3 karakter olmalıdır.";
    public const string ContentRequired = "Yazı içeriği zorunludur.";
    public const string ContentMinLength = "Yazı içeriği en az 10 karakter olmalıdır.";
    public const string AuthorRequired = "Yazar adı zorunludur.";
    public const string AuthorMaxLength = "Yazar adı 100 karakterden uzun olamaz.";
    public const string AuthorMinLength = "Yazar adı en az 2 karakter olmalıdır.";
    public const string CategoryIdRequired = "Kategori Id zorunludur.";
    public const string CategoryIdMustBeValidGuid = "Kategori Id geçerli bir GUID olmalıdır.";
    public const string CoverImageUrlMaxLength = "Kapak resmi URL'si 500 karakterden uzun olamaz.";
    public const string PostImageUrlMaxLength = "Yazı resmi URL'si 500 karakterden uzun olamaz.";
}

