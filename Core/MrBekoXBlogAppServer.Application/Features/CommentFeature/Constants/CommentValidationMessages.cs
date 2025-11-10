namespace MrBekoXBlogAppServer.Application.Features.CommentFeature.Constants;

public class CommentValidationMessages
{
    public const string CommentIdRequired = "Yorum Id zorunludur.";
    public const string CommentIdMustBeValidGuid = "Yorum Id geçerli bir GUID olmalıdır.";
    public const string ContentRequired = "Yorum içeriği zorunludur.";
    public const string ContentMaxLength = "Yorum içeriği 500 karakterden uzun olamaz.";
    public const string ContentMinLength = "Yorum içeriği en az 3 karakter olmalıdır.";
    public const string PostIdRequired = "Yazı Id zorunludur.";
    public const string PostIdMustBeValidGuid = "Yazı Id geçerli bir GUID olmalıdır.";
}

