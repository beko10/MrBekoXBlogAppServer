using Microsoft.EntityFrameworkCore;
using MrBekoXBlogAppServer.Domain.Entities;
using MrBekoXBlogAppServer.Domain.Entities.Common;

namespace MrBekoXBlogAppServer.Persistence.Context;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<ContactInfo> ContactInfos { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<SocialMedia> SocialMedias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditFields();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditFields()
    {
        var entries = ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = DateTime.UtcNow;
                entry.Entity.UpdatedDate = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                // CreatedDate korunur
                entry.Property(x => x.CreatedDate).IsModified = false;
                entry.Entity.UpdatedDate = DateTime.UtcNow;
            }
        }
    }

}
