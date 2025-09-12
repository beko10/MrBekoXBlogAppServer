using MrBekoXBlogAppServer.Application.Interfaces.Repositories.CategoryRepository;
using MrBekoXBlogAppServer.Domain.Entities;
using MrBekoXBlogAppServer.Persistence.Context;

namespace MrBekoXBlogAppServer.Persistence.Repositories.EfCoreCategoryRepository;

public class EfCoreCategoryWriteRepository : EfCoreWriteRepository<Category>, ICategoryWriteRepository
{
    public EfCoreCategoryWriteRepository(AppDbContext context) : base(context)
    {
    }
}
