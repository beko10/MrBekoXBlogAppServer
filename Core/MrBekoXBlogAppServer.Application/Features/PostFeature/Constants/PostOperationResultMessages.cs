namespace MrBekoXBlogAppServer.Application.Features.PostFeature.Constants;

public static class PostOperationResultMessages
{
    // Create
    public const string CreatedSuccess = "Yazı başarıyla oluşturuldu.";
    public const string CreatedFailure = "Yazı oluşturulurken bir hata oluştu.";

    // GetAll
    public const string GetAllSuccess = "Yazılar başarıyla listelendi.";
    public const string GetAllFailure = "Yazılar getirilirken bir hata oluştu.";
    public const string GetAllNotFound = "Hiç yazı bulunamadı.";

    // GetById
    public const string GetByIdSuccess = "Yazı başarıyla getirildi.";
    public const string GetByIdFailure = "Yazı getirilirken bir hata oluştu.";
    public const string GetByIdNotFound = "Yazı bulunamadı.";

    // Update
    public const string UpdatedSuccess = "Yazı başarıyla güncellendi.";
    public const string UpdatedFailure = "Yazı güncellenirken bir hata oluştu.";

    // Delete
    public const string DeletedSuccess = "Yazı başarıyla silindi.";
    public const string DeletedFailure = "Yazı silinirken bir hata oluştu.";
}

