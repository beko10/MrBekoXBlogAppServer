namespace MrBekoXBlogAppServer.Application.Interfaces.Auth;

public interface ITokenBlacklistService
{
    Task BlacklistTokenAsync(string jti, DateTime expiresAt, CancellationToken cancellationToken = default);
    Task<bool> IsTokenBlacklistedAsync(string jti, CancellationToken cancellationToken = default);
    Task CleanupExpiredTokensAsync(CancellationToken cancellationToken = default);
}

