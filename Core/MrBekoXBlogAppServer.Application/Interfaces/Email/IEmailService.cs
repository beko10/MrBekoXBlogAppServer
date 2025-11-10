namespace MrBekoXBlogAppServer.Application.Interfaces.Email;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body, bool isHtml = true);
    Task SendActivationEmailAsync(string to, string userName, string activationToken);
}
