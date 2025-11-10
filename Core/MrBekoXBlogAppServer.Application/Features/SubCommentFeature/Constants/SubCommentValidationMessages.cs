namespace MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Constants;

public class SubCommentValidationMessages
{
    public const string SubCommentIdRequired = "Alt yorum Id zorunludur.";
    public const string SubCommentIdMustBeValidGuid = "Alt yorum Id geçerli bir GUID olmalıdır.";
    public const string ContentRequired = "Alt yorum içeriği zorunludur.";
    public const string ContentMaxLength = "Alt yorum içeriği 500 karakterden uzun olamaz.";
    public const string ContentMinLength = "Alt yorum içeriği en az 3 karakter olmalıdır.";
    public const string CommentIdRequired = "Yorum Id zorunludur.";
    public const string CommentIdMustBeValidGuid = "Yorum Id geçerli bir GUID olmalıdır.";
}

