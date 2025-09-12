using MrBekoXBlogAppServer.Application.Interfaces.Repositories.SocialMediaRepository;
using MrBekoXBlogAppServer.Domain.Entities;
using MrBekoXBlogAppServer.Persistence.Context;

namespace MrBekoXBlogAppServer.Persistence.Repositories.EfCoreSocialMediaRepository;

public class EfCoreSocialMediaReadRepository : EfCoreReadRepository<SocialMedia>, ISocialMediaReadRepository
{
    public EfCoreSocialMediaReadRepository(AppDbContext context) : base(context)
    {
    }
}
