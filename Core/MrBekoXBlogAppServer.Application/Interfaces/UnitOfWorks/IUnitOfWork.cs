namespace MrBekoXBlogAppServer.Application.Interfaces.UnitOfWorks;

public interface IUnitOfWork : IAsyncDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
