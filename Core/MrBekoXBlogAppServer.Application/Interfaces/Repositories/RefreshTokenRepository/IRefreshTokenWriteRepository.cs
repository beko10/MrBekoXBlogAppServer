using MrBekoXBlogAppServer.Domain.Entities;

namespace MrBekoXBlogAppServer.Application.Interfaces.Repositories.RefreshTokenRepository;

public interface IRefreshTokenWriteRepository : IWriteRepository<RefreshToken>
{
    public Task RevokeAllAsync(string userId, string reason,
CancellationToken cancellationToken = default);
}
