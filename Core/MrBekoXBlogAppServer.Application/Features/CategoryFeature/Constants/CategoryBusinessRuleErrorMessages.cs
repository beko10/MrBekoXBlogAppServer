namespace MrBekoXBlogAppServer.Application.Features.CategoryFeature.Constants;

public static class CategoryBusinessRuleErrorMessages
{
    public const string NameEmpty = "Kategori adı boş olamaz.";
    public const string NameTooShort = "Kategori adı en az 2 karakter olmalıdır.";
    public const string NameTooLong = "Kategori adı en fazla 100 karakter olabilir.";
    public const string NameAlreadyExists = "Bu isimde zaten bir kategori mevcut.";
    public const string NotFound = "Kategori bulunamadı.";
    public const string HasPosts = "Kategoriye bağlı postlar bulunduğu için silinemez.";
    public const string CategoryLimitExceeded = "Kategori sayısı izin verilen limiti aştı.";
}
