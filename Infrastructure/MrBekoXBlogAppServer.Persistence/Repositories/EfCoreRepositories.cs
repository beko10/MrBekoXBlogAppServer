using Microsoft.EntityFrameworkCore;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories;
using MrBekoXBlogAppServer.Domain.Entities.Common;
using MrBekoXBlogAppServer.Persistence.Context;

namespace MrBekoXBlogAppServer.Persistence.Repositories;

public class EfCoreRepositories<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly AppDbContext _context;
    protected DbSet<TEntity> _dbSet => _context.Set<TEntity>();
    public EfCoreRepositories(AppDbContext context)
    {
        _context = context;
    }

}
