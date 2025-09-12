using MrBekoXBlogAppServer.Application.Interfaces.Repositories.MessageRepository;
using MrBekoXBlogAppServer.Domain.Entities;
using MrBekoXBlogAppServer.Persistence.Context;

namespace MrBekoXBlogAppServer.Persistence.Repositories.EfCoreMessageRepository;

public class EfCoreMessageWriteRepository : EfCoreWriteRepository<Message>, IMessageWriteRepository
{
    public EfCoreMessageWriteRepository(AppDbContext context) : base(context)
    {
    }
}
