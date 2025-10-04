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
        // Belleğe çekmeden, doğrudan veritabanında UPDATE sorgusu çalıştır.
        await _dbSet
            .Where(rt => rt.UserId == userId && 
                rt.RevokedAt == null && 
                rt.ExpiresAt > DateTime.UtcNow
            )
            .ExecuteUpdateAsync(updates => updates
                .SetProperty(rt => rt.RevokedAt, DateTime.UtcNow)
                .SetProperty(rt => rt.RevokedReason, reason),
                cancellationToken
             );
    }
}
