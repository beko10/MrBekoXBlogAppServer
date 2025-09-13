using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MrBekoXBlogAppServer.Domain.Entities;

namespace MrBekoXBlogAppServer.Persistence.Configuration;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");

        //Primary Key
        builder.HasKey(p => p.Id);
        // Title zorunlu
        builder.Property(p => p.Title)
               .IsRequired()
               .HasMaxLength(200);

        // Content zorunlu
        builder.Property(p => p.Content)
               .IsRequired();

        // 1 Post → 1 Category
        builder.HasOne(p => p.Category)
               .WithMany(c => c.Posts)
               .HasForeignKey(p => p.CategoryId)
               .OnDelete(DeleteBehavior.Cascade);

        // Seed data
        builder.HasData(
            new Post
            {
                Id = "30b81e45-a012-440d-bf6f-1a56a2e3d50f",
                Title = "Minimal API ile Onion Architecture",
                Content = "ASP.NET Core 9 ile Minimal API ve Onion Architecture kullanımı...",
                Author = "Berkay",
                CoverImageUrl = "/images/cover1.jpg",
                PostImageUrl = "/images/post1.jpg",
                PublishedDate = new DateTime(2025, 09, 13, 0, 0, 0, DateTimeKind.Utc), 
                CategoryId = "31e68485-dac5-4ac2-a76d-72c80838a109",
                CreatedDate = new DateTime(2025, 09, 13, 0, 0, 0, DateTimeKind.Utc),
                UpdatedDate = new DateTime(2025, 09, 13, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}

