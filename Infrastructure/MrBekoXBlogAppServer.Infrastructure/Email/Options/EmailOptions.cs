namespace MrBekoXBlogAppServer.Infrastructure.Email.Options;

public class EmailOptions
{
    public const string SectionName = "Email";
    public string SmtpHost { get; init; } = default!;
    public int SmtpPort { get; init; }
    public string SmtpUsername { get; init; } = default!;
    public string SmtpPassword { get; init; } = default!;
    public string FromEmail { get; init; } = default!;
    public string FromName { get; init; } = default!;
    public bool EnableSsl { get; init; } = true;
}
