using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MrBekoXBlogAppServer.Domain.Entities;

namespace MrBekoXBlogAppServer.Persistence.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        // Primary Key
        builder.HasKey(c => c.Id);

        // CategoryName zorunlu
        builder.Property(c => c.CategoryName)
               .IsRequired()
               .HasMaxLength(100);

        // 1 Category → N Post
        builder.HasMany(c => c.Posts)
               .WithOne(p => p.Category)
               .HasForeignKey(p => p.CategoryId)
               .OnDelete(DeleteBehavior.Cascade);

        // Seed data
        builder.HasData(
            new Category 
            { 
                Id = "31e68485-dac5-4ac2-a76d-72c80838a109", 
                CategoryName = "Yazılım",
                CreatedDate = new DateTime(2025, 09, 13, 0, 0, 0, DateTimeKind.Utc),
                UpdatedDate = new DateTime(2025, 09, 13, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category 
            { 
                Id = "43b62264-27ed-45bc-8714-8a887c1d108b", 
                CategoryName = "Teknoloji",
                CreatedDate = new DateTime(2025, 09, 13, 0, 0, 0, DateTimeKind.Utc),
                UpdatedDate = new DateTime(2025, 09, 13, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category 
            { 
                Id = "01c7b4fc-2898-4a80-af7f-b018311efc35", 
                CategoryName = "Yaşam",
                CreatedDate = new DateTime(2025, 09, 13, 0, 0, 0, DateTimeKind.Utc),
                UpdatedDate = new DateTime(2025, 09, 13, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
