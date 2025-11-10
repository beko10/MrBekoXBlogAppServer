namespace MrBekoXBlogAppServer.Application.Features.CommentFeature.Constants;

public static class CommentOperationResultMessages
{
    // Create
    public const string CreatedSuccess = "Yorum başarıyla oluşturuldu.";
    public const string CreatedFailure = "Yorum oluşturulurken bir hata oluştu.";

    // GetAll
    public const string GetAllSuccess = "Yorumlar başarıyla listelendi.";
    public const string GetAllFailure = "Yorumlar getirilirken bir hata oluştu.";
    public const string GetAllNotFound = "Hiç yorum bulunamadı.";

    // GetById
    public const string GetByIdSuccess = "Yorum başarıyla getirildi.";
    public const string GetByIdFailure = "Yorum getirilirken bir hata oluştu.";
    public const string GetByIdNotFound = "Yorum bulunamadı.";

    // Update
    public const string UpdatedSuccess = "Yorum başarıyla güncellendi.";
    public const string UpdatedFailure = "Yorum güncellenirken bir hata oluştu.";

    // Delete
    public const string DeletedSuccess = "Yorum başarıyla silindi.";
    public const string DeletedFailure = "Yorum silinirken bir hata oluştu.";
}

