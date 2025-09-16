using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MrBekoXBlogAppServer.Domain.Entities;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(p => p.Content).IsRequired();

        // Post → Category (Cascade)
        builder.HasOne(p => p.Category)
               .WithMany(c => c.Posts)
               .HasForeignKey(p => p.CategoryId)
               .OnDelete(DeleteBehavior.Cascade);

        // Post → User (Cascade bırakılabilir)
        builder.HasOne(p => p.User)
               .WithMany(u => u.Posts)
               .HasForeignKey(p => p.UserId)
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
                PublishedDate = new DateTime(2025, 09, 14, 0, 0, 0, DateTimeKind.Utc),
                CategoryId = "a1e8c1f0-1f0c-4b77-9c7b-1d6b4a3c5e11",
                UserId = "u1234567-89ab-cdef-0123-456789abcdef",
                CreatedDate = new DateTime(2025, 09, 14, 0, 0, 0, DateTimeKind.Utc),
                UpdatedDate = new DateTime(2025, 09, 14, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
