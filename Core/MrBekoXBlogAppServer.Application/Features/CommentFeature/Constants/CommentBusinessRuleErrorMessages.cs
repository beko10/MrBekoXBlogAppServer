namespace MrBekoXBlogAppServer.Application.Features.CommentFeature.Constants;

public static class CommentBusinessRuleErrorMessages
{
    public const string ContentEmpty = "Yorum içeriği boş olamaz.";
    public const string ContentTooShort = "Yorum içeriği en az 3 karakter olmalıdır.";
    public const string ContentTooLong = "Yorum içeriği en fazla 500 karakter olabilir.";
    public const string NotFound = "Yorum bulunamadı.";
    public const string PostNotFound = "Yorum yapılacak yazı bulunamadı.";
    public const string UserNotFound = "Kullanıcı bulunamadı.";
    public const string CannotDeleteIfHasSubComments = "Yoruma bağlı alt yorumlar bulunduğu için silinemez.";
}

