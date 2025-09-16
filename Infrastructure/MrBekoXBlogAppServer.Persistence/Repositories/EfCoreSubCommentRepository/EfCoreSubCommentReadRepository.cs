using MrBekoXBlogAppServer.Application.Interfaces.Repositories.SubCommentRepository;
using MrBekoXBlogAppServer.Domain.Entities;
using MrBekoXBlogAppServer.Persistence.Context;

namespace MrBekoXBlogAppServer.Persistence.Repositories.EfCoreSubCommentRepository;

public class EfCoreSubCommentReadRepository : EfCoreReadRepository<SubComment>, ISubCommentReadRepository
{
    public EfCoreSubCommentReadRepository(AppDbContext context) : base(context)
    {
    }
}
