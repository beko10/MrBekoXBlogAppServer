using MrBekoXBlogAppServer.Application.Interfaces.Repositories.CommentRepository;
using MrBekoXBlogAppServer.Domain.Entities;
using MrBekoXBlogAppServer.Persistence.Context;

namespace MrBekoXBlogAppServer.Persistence.Repositories.EfCoreCommentRepository;

public class EfCoreCommentReadRepository : EfCoreReadRepository<Comment>, ICommentReadRepository
{
    public EfCoreCommentReadRepository(AppDbContext context) : base(context)
    {
    }
}
