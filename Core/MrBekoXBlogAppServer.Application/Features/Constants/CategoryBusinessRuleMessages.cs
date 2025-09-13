namespace MrBekoXBlogAppServer.Application.Features.CategoryFeature.Constants;

public static class CategoryBusinessRuleMessages
{
    public const string NameEmpty = "Kategori adı boş olamaz";
    public const string NameTooShort = "Kategori adı en az 2 karakter olmalıdır";
    public const string NameTooLong = "Kategori adı en fazla 100 karakter olabilir";
    public const string NameAlreadyExists = "Bu kategori adı zaten mevcut";
    public const string NotFound = "Kategori bulunamadı";
    public const string HasPosts = "Bu kategoriye bağlı yazılar var, silinemez.";
    public const string CategoryLimitExceeded = "Maksimum kategori limitine ulaşıldı";
}
