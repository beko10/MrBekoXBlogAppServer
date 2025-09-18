using Microsoft.EntityFrameworkCore;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.RefreshTokenRepository;
using MrBekoXBlogAppServer.Domain.Entities;
using MrBekoXBlogAppServer.Persistence.Context;

namespace MrBekoXBlogAppServer.Persistence.Repositories.EfCoreRefreshTokenRepository;

public class EfCoreRefreshTokenWriteRepository : EfCoreWriteRepository<RefreshToken>, IRefreshTokenWriteRepository
{
    public EfCoreRefreshTokenWriteRepository(AppDbContext context) : base(context)
    {
    }

    public async Task RevokeAllAsync(string userId, string reason, CancellationToken cancellationToken = default)
    {
        var tokens = await _dbSet.Where(rt => rt.UserId == userId && rt.RevokedAt == null && rt.ExpiresAt > DateTime.UtcNow).ToListAsync(cancellationToken);
        foreach (var token in tokens)
        {
            token.RevokedAt = DateTime.UtcNow;
            token.RevokedReason = reason;
        }
    }
}
