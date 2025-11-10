using MrBekoXBlogAppServer.Application.Common.DTOs.Security;

namespace MrBekoXBlogAppServer.Application.Interfaces.Auth;

public interface ISessionManagementService
{
    Task<List<ActiveSessionDto>> GetActiveSessionsAsync(string userId, CancellationToken cancellationToken = default);
    Task RevokeSessionAsync(string userId, string refreshTokenHash, CancellationToken cancellationToken = default);
    Task RevokeAllSessionsAsync(string userId, CancellationToken cancellationToken = default);
    Task<bool> CanCreateNewSessionAsync(string userId, int maxConcurrentSessions = 5, CancellationToken cancellationToken = default);
}

