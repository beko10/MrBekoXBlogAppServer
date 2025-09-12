using MrBekoXBlogAppServer.Application.Interfaces.Repositories;
using MrBekoXBlogAppServer.Domain.Entities;

namespace MrBekoXBlogAppServer.Application.Interfaces.Repositories.PostRepository;

public interface IPostWriteRepository : IWriteRepository<Post>
{
}
