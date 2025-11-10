using MrBekoXBlogAppServer.Application.Common.DTOs.Security;

namespace MrBekoXBlogAppServer.Application.Interfaces.Auth;

public interface IJwtTokenService
{
  Task<TokenDto> CreateAccessTokenAsync(
      string userId,
      string? deviceInfo = null,
      string? ipAddress = null,
      string? userAgent = null,
      CancellationToken cancellationToken = default
    );

  Task<TokenDto> RefreshAsync(
      string refreshTokenRaw,
      CancellationToken cancellationToken = default
    );
}
