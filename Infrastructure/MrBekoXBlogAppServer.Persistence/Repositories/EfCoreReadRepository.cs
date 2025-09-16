using Microsoft.EntityFrameworkCore;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories;
using MrBekoXBlogAppServer.Domain.Entities.Common;
using MrBekoXBlogAppServer.Persistence.Context;
using System.Linq.Expressions;

namespace MrBekoXBlogAppServer.Persistence.Repositories;

public  class EfCoreReadRepository<TEntity> : EfCoreRepositories<TEntity>, IReadRepository<TEntity> where TEntity : BaseEntity
{
    public EfCoreReadRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<int> CountAsync(bool tracking = true,CancellationToken cancellationToken = default)
    {
        return await _dbSet.CountAsync(cancellationToken);
    }

    public async Task<int> CountWhereAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, bool tracking = true)
    {
        return await _dbSet.CountAsync(predicate, cancellationToken);
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, bool tracking = true)
    {
        return await _dbSet.AnyAsync(predicate, cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(
        bool tracking = true,
        bool autoInclude = true,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsQueryable();   
        if (!tracking)
        {
            query = query.AsNoTracking();
        }

        if(autoInclude)
        {
            query = query.IgnoreAutoIncludes();
        }
        
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllWithIncludesAsync(bool tracking = true,params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();   
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(
        string id, bool tracking = true,
        bool autoInclude = true, 
        CancellationToken cancellationToken = default
       )
    {
        var query = _dbSet.AsQueryable();  
        if (!tracking)
        {
            query = _dbSet.AsNoTracking();
        }
        if(autoInclude)
        {
            query = query.IgnoreAutoIncludes();
        }

        return await _dbSet.FindAsync(id, cancellationToken);
    }
    public async Task<TEntity> GetByIdWithIncludesAsync(string id,bool tracker=false ,CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        if(!tracker)
        {
            query = query.AsNoTracking();
        }
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }



    public async Task<TEntity> GetSingleAsync(
        Expression<Func<TEntity, bool>> expression, 
        bool tracking = true, 
        bool autoInclude = true,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsQueryable();
        if (!tracking)
        {
            query = query.AsNoTracking();
        }
        if(autoInclude)
        {
            query = query.IgnoreAutoIncludes();
        }

        return await query.FirstOrDefaultAsync(expression, cancellationToken);
    }

    public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> expression, bool tracking = true, CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsQueryable();
        if (!tracking)
        {
            query = query.AsNoTracking();
        }
        return query.Where(expression);
    }
}
