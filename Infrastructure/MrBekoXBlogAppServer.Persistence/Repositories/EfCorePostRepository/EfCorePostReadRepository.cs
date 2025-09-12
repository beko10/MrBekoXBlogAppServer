using MrBekoXBlogAppServer.Application.Interfaces.Repositories.PostRepository;
using MrBekoXBlogAppServer.Domain.Entities;
using MrBekoXBlogAppServer.Persistence.Context;

namespace MrBekoXBlogAppServer.Persistence.Repositories.EfCorePostRepository;

public class EfCorePostReadRepository : EfCoreReadRepository<Post>, IPostReadRepository
{
    public EfCorePostReadRepository(AppDbContext context) : base(context)
    {
    }
}
