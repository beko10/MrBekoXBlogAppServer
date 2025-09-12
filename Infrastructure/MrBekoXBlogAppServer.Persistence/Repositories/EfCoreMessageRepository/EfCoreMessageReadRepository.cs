using MrBekoXBlogAppServer.Application.Interfaces.Repositories.MessageRepository;
using MrBekoXBlogAppServer.Domain.Entities;
using MrBekoXBlogAppServer.Persistence.Context;

namespace MrBekoXBlogAppServer.Persistence.Repositories.EfCoreMessageRepository;

public class EfCoreMessageReadRepository : EfCoreReadRepository<Message>, IMessageReadRepository
{
    public EfCoreMessageReadRepository(AppDbContext context) : base(context)
    {
    }
}
