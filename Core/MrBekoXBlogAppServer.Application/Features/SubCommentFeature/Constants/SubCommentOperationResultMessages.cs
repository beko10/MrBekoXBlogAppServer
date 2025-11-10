namespace MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Constants;

public static class SubCommentOperationResultMessages
{
    // Create
    public const string CreatedSuccess = "Alt yorum başarıyla oluşturuldu.";
    public const string CreatedFailure = "Alt yorum oluşturulurken bir hata oluştu.";

    // GetAll
    public const string GetAllSuccess = "Alt yorumlar başarıyla listelendi.";
    public const string GetAllFailure = "Alt yorumlar getirilirken bir hata oluştu.";
    public const string GetAllNotFound = "Hiç alt yorum bulunamadı.";

    // GetById
    public const string GetByIdSuccess = "Alt yorum başarıyla getirildi.";
    public const string GetByIdFailure = "Alt yorum getirilirken bir hata oluştu.";
    public const string GetByIdNotFound = "Alt yorum bulunamadı.";

    // Update
    public const string UpdatedSuccess = "Alt yorum başarıyla güncellendi.";
    public const string UpdatedFailure = "Alt yorum güncellenirken bir hata oluştu.";

    // Delete
    public const string DeletedSuccess = "Alt yorum başarıyla silindi.";
    public const string DeletedFailure = "Alt yorum silinirken bir hata oluştu.";
}

