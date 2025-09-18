namespace MrBekoXBlogAppServer.Application.Common.DTOs.Security;

public class RefreshTokenDto
{
    public string Token { get; init; } = default!;
    public DateTime ExpiresAt { get; init; }
}
