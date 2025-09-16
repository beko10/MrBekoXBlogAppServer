namespace MrBekoXBlogAppServer.Application.Features.ContactInfoFeature.Constants;

public static class ContactInfoBusinessRuleErrorMessages
{
    public const string EmailAlreadyExists = "Bu email adresi ile zaten bir iletişim bilgisi kayıtlı.";
    public const string PhoneAlreadyExists = "Bu telefon numarası ile zaten bir iletişim bilgisi kayıtlı.";
    public const string ContactInfoAlreadyExists = "Sistemde zaten bir iletişim bilgisi mevcut. Birden fazla iletişim bilgisi oluşturulamaz.";
    public const string ContactInfoNotFound = "İletişim bilgisi bulunamadı.";
}
