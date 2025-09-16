namespace MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.Constants;

public static class ContactInfoOperationResultMessages
{
    #region Başarılı Mesajlar

    // Create
    public const string CreatedSuccess = "İletişim bilgisi başarıyla oluşturuldu";

    // Update  
    public const string UpdatedSuccess = "İletişim bilgisi başarıyla güncellendi";

    // Delete
    public const string DeletedSuccess = "İletişim bilgisi başarıyla silindi";

    // Get Operations
    public const string RetrievedSuccess = "İletişim bilgisi başarıyla getirildi";
    public const string ListRetrievedSuccess = "İletişim bilgileri listesi başarıyla getirildi";

    #endregion

    #region Hata Mesajları

    // Not Found
    public const string NotFound = "İletişim bilgisi bulunamadı";

    // Operation Failures
    public const string CreateFailed = "İletişim bilgisi oluşturulamadı";
    public const string UpdateFailed = "İletişim bilgisi güncellenemedi";
    public const string DeleteFailed = "İletişim bilgisi silinemedi";
    public const string RetrieveFailed = "İletişim bilgisi getirilemedi";

    // General Failures
    public const string OperationFailed = "İletişim bilgisi işlemi başarısız oldu";
    public const string InvalidRequest = "Geçersiz iletişim bilgisi isteği";

    #endregion

    #region Bilgi Mesajları

    public const string NoContactInfoFound = "Herhangi bir iletişim bilgisi kaydı bulunamadı";
    public const string ContactInfoListEmpty = "İletişim bilgileri listesi boş";

    #endregion
}
