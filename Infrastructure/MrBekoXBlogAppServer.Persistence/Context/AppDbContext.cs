using Microsoft.EntityFrameworkCore;
using MrBekoXBlogAppServer.Domain.Entities;

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


}
