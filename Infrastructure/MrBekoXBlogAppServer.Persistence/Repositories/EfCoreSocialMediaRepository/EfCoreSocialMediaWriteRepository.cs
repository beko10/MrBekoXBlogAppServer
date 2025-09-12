using MrBekoXBlogAppServer.Application.Interfaces.Repositories.SocialMediaRepository;
using MrBekoXBlogAppServer.Domain.Entities;
using MrBekoXBlogAppServer.Persistence.Context;

namespace MrBekoXBlogAppServer.Persistence.Repositories.EfCoreSocialMediaRepository;

public class EfCoreSocialMediaWriteRepository : EfCoreWriteRepository<SocialMedia>, ISocialMediaWriteRepository
{
    public EfCoreSocialMediaWriteRepository(AppDbContext context) : base(context)
    {
    }
}
