namespace MrBekoXBlogAppServer.Application.Features.Constants;

public static class CategoryOperationResultMessages
{
    // Create
    public const string CreatedSuccess = "Kategori başarıyla oluşturuldu.";
    public const string CreatedFailure = "Kategori oluşturulurken bir hata oluştu.";

    // GetAll
    public const string GetAllSuccess = "Kategoriler başarıyla listelendi.";
    public const string GetAllFailure = "Kategoriler getirilirken bir hata oluştu.";
    public const string GetAllNotFound = "Hiç kategori bulunamadı.";

    // GetById
    public const string GetByIdSuccess = "Kategori başarıyla getirildi.";
    public const string GetByIdFailure = "Kategori getirilirken bir hata oluştu.";
    public const string GetByIdNotFound = "Kategori bulunamadı.";

    // Update
    public const string UpdatedSuccess = "Kategori başarıyla güncellendi.";
    public const string UpdatedFailure = "Kategori güncellenirken bir hata oluştu.";

    // Delete
    public const string DeletedSuccess = "Kategori başarıyla silindi.";
    public const string DeletedFailure = "Kategori silinirken bir hata oluştu.";
}
