namespace MrBekoXBlogAppServer.Application.Common.DTOs.Security;

public class JwtAccessTokenDto
{
    public string Token { get; init; } = default!;
    public DateTime ExpirationAt { get; init; }
    public string Jti { get; init; } = default!;
}
