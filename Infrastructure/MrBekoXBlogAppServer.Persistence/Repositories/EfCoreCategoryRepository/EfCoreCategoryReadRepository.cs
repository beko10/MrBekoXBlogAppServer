using MrBekoXBlogAppServer.Application.Interfaces.Repositories.CategoryRepository;
using MrBekoXBlogAppServer.Domain.Entities;
using MrBekoXBlogAppServer.Persistence.Context;

namespace MrBekoXBlogAppServer.Persistence.Repositories.EfCoreCategoryRepository;

public class EfCoreCategoryReadRepository : EfCoreReadRepository<Category>, ICategoryReadRepository
{
    public EfCoreCategoryReadRepository(AppDbContext context) : base(context)
    {
    }
}
