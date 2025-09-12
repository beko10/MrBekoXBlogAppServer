using MrBekoXBlogAppServer.Application.Interfaces.Repositories;
using MrBekoXBlogAppServer.Domain.Entities;

namespace MrBekoXBlogAppServer.Application.Interfaces.Repositories.MessageRepository;

public interface IMessageReadRepository : IReadRepository<Message>
{
}
