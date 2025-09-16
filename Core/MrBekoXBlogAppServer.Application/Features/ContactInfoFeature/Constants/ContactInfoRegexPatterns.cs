namespace MrBekoXBlogAppServer.Application.Features.ContactInfoFeature.Constants;

public class ContactInfoRegexPatterns
{
    public const string Phone = @"^[\+]?[0-9\s\-\(\)]+$";
    public const string Email = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
    public const string Url = @"^https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)$";
}
