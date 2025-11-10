using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using MrBekoXBlogAppServer.Application.Interfaces.Email;
using MrBekoXBlogAppServer.Infrastructure.Email.Options;

namespace MrBekoXBlogAppServer.Infrastructure.Email;

public class EmailService(IOptions<EmailOptions> options) : IEmailService
{
    private readonly EmailOptions _options = options.Value;

    public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = true)
    {
        using var client = new SmtpClient(_options.SmtpHost, _options.SmtpPort)
        {
            EnableSsl = _options.EnableSsl,
            Credentials = new NetworkCredential(_options.SmtpUsername, _options.SmtpPassword)
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_options.FromEmail, _options.FromName),
            Subject = subject,
            Body = body,
            IsBodyHtml = isHtml
        };

        mailMessage.To.Add(to);

        await client.SendMailAsync(mailMessage);
    }

    public async Task SendActivationEmailAsync(string to, string userName, string activationToken)
    {
        var subject = "Hesabınızı Aktifleştirin - MrBekoX Blog";

        var body = $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background-color: #4CAF50; color: white; padding: 20px; text-align: center; }}
        .content {{ padding: 20px; background-color: #f9f9f9; }}
        .button {{ display: inline-block; padding: 12px 24px; background-color: #4CAF50; color: white; text-decoration: none; border-radius: 4px; margin: 20px 0; }}
        .footer {{ text-align: center; padding: 20px; color: #666; font-size: 12px; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>Hoş Geldiniz!</h1>
        </div>
        <div class='content'>
            <p>Merhaba <strong>{userName}</strong>,</p>
            <p>MrBekoX Blog'a kaydolduğunuz için teşekkür ederiz. Hesabınızı aktifleştirmek için aşağıdaki linke tıklayın:</p>
            <div style='text-align: center;'>
                <a href='http://localhost:8080/api/auth/confirm-email?token={activationToken}' class='button'>Hesabı Aktifleştir</a>
            </div>
            <p>Veya aşağıdaki kodu kullanabilirsiniz:</p>
            <p style='background-color: #eee; padding: 10px; font-family: monospace;'>{activationToken}</p>
            <p>Bu link 24 saat geçerlidir.</p>
            <p>Eğer bu kaydı siz yapmadıysanız, bu e-postayı görmezden gelebilirsiniz.</p>
        </div>
        <div class='footer'>
            <p>&copy; 2025 MrBekoX Blog. Tüm hakları saklıdır.</p>
        </div>
    </div>
</body>
</html>";

        await SendEmailAsync(to, subject, body, isHtml: true);
    }
}
