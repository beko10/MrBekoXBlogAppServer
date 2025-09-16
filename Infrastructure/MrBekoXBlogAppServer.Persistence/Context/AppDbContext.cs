using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MrBekoXBlogAppServer.Domain.Entities;
using MrBekoXBlogAppServer.Domain.Entities.Common;

namespace MrBekoXBlogAppServer.Persistence.Context;

public sealed class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<ContactInfo> ContactInfos { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<SocialMedia> SocialMedias { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<SubComment> SubComments { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        //AppUser → Posts, Comments, SubComments
        modelBuilder.Entity<AppUser>()
            .Navigation(u => u.Posts)
            .AutoInclude();

        modelBuilder.Entity<AppUser>()
            .Navigation(u => u.Comments)
            .AutoInclude();

        modelBuilder.Entity<AppUser>()
            .Navigation(u => u.SubComments)
            .AutoInclude();

        //Post → Category, User, Comments
        modelBuilder.Entity<Post>()
            .Navigation(p => p.Category)
            .AutoInclude();

        modelBuilder.Entity<Post>()
            .Navigation(p => p.User)
            .AutoInclude();

        modelBuilder.Entity<Post>()
            .Navigation(p => p.Comments)
            .AutoInclude();

        //Comment → Post, User, SubComments
        modelBuilder.Entity<Comment>()
            .Navigation(c => c.Post)
            .AutoInclude();

        modelBuilder.Entity<Comment>()
            .Navigation(c => c.User)
            .AutoInclude();

        modelBuilder.Entity<Comment>()
            .Navigation(c => c.SubComments)
            .AutoInclude();

        //SubComment → Comment, User
        modelBuilder.Entity<SubComment>()
            .Navigation(sc => sc.Comment)
            .AutoInclude();

        modelBuilder.Entity<SubComment>()
            .Navigation(sc => sc.User)
            .AutoInclude();

        //Category → Posts (opsiyonel, sık lazımsa açabilirsin)
        modelBuilder.Entity<Category>()
            .Navigation(c => c.Posts)
            .AutoInclude();

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
                entry.Property(x => x.CreatedDate).IsModified = false;
                entry.Entity.UpdatedDate = DateTime.UtcNow;
            }
        }
    }

}
