using MrBekoXBlogAppServer.Application.Interfaces.Repositories.SubCommentRepository;
using MrBekoXBlogAppServer.Domain.Entities;
using MrBekoXBlogAppServer.Persistence.Context;

namespace MrBekoXBlogAppServer.Persistence.Repositories.EfCoreSubCommentRepository;

public class EfCoreSubCommentWriteRepository : EfCoreWriteRepository<SubComment>, ISubCommentWriteRepository
{
    public EfCoreSubCommentWriteRepository(AppDbContext context) : base(context)
    {
    }
}
