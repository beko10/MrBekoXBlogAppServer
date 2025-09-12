using MrBekoXBlogAppServer.Application.Interfaces.Repositories.PostRepository;
using MrBekoXBlogAppServer.Domain.Entities;
using MrBekoXBlogAppServer.Persistence.Context;

namespace MrBekoXBlogAppServer.Persistence.Repositories.EfCorePostRepository;

public class EfCorePostWriteRepository : EfCoreWriteRepository<Post>, IPostWriteRepository
{
    public EfCorePostWriteRepository(AppDbContext context) : base(context)
    {
    }
}
