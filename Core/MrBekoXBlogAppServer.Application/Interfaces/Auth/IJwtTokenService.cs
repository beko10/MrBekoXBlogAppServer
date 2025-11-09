using MrBekoXBlogAppServer.Application.Common.DTOs.Security;

namespace MrBekoXBlogAppServer.Application.Interfaces.Auth;

public interface IJwtTokenService
{
    Task<TokenDto> CreateAccessTokenAsync(
        string userId, 
        CancellationToken cancellationToken = default
      );

    Task<TokenDto> RefreshAsync(string refreshTokenRaw, 
        CancellationToken cancellationToken = default
      );

    
}
