namespace MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Constants;

public static class SubCommentBusinessRuleErrorMessages
{
    public const string ContentEmpty = "Alt yorum içeriği boş olamaz.";
    public const string ContentTooShort = "Alt yorum içeriği en az 3 karakter olmalıdır.";
    public const string ContentTooLong = "Alt yorum içeriği en fazla 500 karakter olabilir.";
    public const string NotFound = "Alt yorum bulunamadı.";
    public const string CommentNotFound = "Alt yorum yapılacak yorum bulunamadı.";
    public const string UserNotFound = "Kullanıcı bulunamadı.";
}

