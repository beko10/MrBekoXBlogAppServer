namespace MrBekoXBlogAppServer.Application.Features.CategoryFeature.Constants;

public class CategoryValidationMessages
{
    public const string CategoryIdRequired = "Kategori Id zorunludur.";
    public const string CategoryIdMustBeValidGuid = "Kategori Id geçerli bir GUID olmalıdır.";
    public const string CategoryNameRequired = "Kategori adı zorunludur.";
    public const string CategoryNameMaxLength = "Kategori adı 100 karakterden uzun olamaz.";
}
