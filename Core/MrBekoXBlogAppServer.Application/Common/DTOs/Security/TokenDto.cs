namespace MrBekoXBlogAppServer.Application.Common.DTOs.Security;

public class TokenDto
{
    public JwtAccessTokenDto AccessToken { get; init; } = default!;
    public RefreshTokenDto RefreshToken { get; init; } = default!;
}
