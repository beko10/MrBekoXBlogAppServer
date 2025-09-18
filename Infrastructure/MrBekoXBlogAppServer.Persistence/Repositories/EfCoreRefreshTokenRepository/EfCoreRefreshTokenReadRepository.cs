using MrBekoXBlogAppServer.Application.Interfaces.Repositories.RefreshTokenRepository;
using MrBekoXBlogAppServer.Domain.Entities;
using MrBekoXBlogAppServer.Persistence.Context;

namespace MrBekoXBlogAppServer.Persistence.Repositories.EfCoreRefreshTokenRepository;

public class EfCoreRefreshTokenReadRepository : EfCoreReadRepository<RefreshToken>, IRefreshTokenReadRepository
{
    public EfCoreRefreshTokenReadRepository(AppDbContext context) : base(context)
    {
    }
}
