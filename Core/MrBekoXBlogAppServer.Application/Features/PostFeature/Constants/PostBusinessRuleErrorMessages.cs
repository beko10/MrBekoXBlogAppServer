namespace MrBekoXBlogAppServer.Application.Features.PostFeature.Constants;

public static class PostBusinessRuleErrorMessages
{
    public const string TitleEmpty = "Yazı başlığı boş olamaz.";
    public const string TitleTooShort = "Yazı başlığı en az 3 karakter olmalıdır.";
    public const string TitleTooLong = "Yazı başlığı en fazla 200 karakter olabilir.";
    public const string ContentEmpty = "Yazı içeriği boş olamaz.";
    public const string ContentTooShort = "Yazı içeriği en az 10 karakter olmalıdır.";
    public const string AuthorEmpty = "Yazar adı boş olamaz.";
    public const string AuthorTooShort = "Yazar adı en az 2 karakter olmalıdır.";
    public const string AuthorTooLong = "Yazar adı en fazla 100 karakter olabilir.";
    public const string NotFound = "Yazı bulunamadı.";
    public const string CategoryNotFound = "Kategori bulunamadı.";
    public const string CannotDeleteIfHasComments = "Yazıya bağlı yorumlar bulunduğu için silinemez.";
}

