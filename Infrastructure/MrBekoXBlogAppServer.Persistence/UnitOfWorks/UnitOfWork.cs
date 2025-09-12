using MrBekoXBlogAppServer.Application.Interfaces.UnitOfWorks;
using MrBekoXBlogAppServer.Persistence.Context;

namespace MrBekoXBlogAppServer.Persistence.UnitOfWorks;

public class UnitOfWork(AppDbContext _context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)=> await _context.SaveChangesAsync(cancellationToken);

    public async ValueTask DisposeAsync() => await _context.DisposeAsync();


}
