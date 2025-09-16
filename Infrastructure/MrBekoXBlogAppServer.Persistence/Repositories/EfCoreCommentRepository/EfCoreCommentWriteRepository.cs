using MrBekoXBlogAppServer.Application.Interfaces.Repositories.CommentRepository;
using MrBekoXBlogAppServer.Domain.Entities;
using MrBekoXBlogAppServer.Persistence.Context;

namespace MrBekoXBlogAppServer.Persistence.Repositories.EfCoreCommentRepository;

public class EfCoreCommentWriteRepository : EfCoreWriteRepository<Comment>, ICommentWriteRepository
{
    public EfCoreCommentWriteRepository(AppDbContext context) : base(context)
    {
    }
}
