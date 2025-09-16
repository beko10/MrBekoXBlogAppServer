using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MrBekoXBlogAppServer.Domain.Entities;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.CategoryName)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasMany(c => c.Posts)
               .WithOne(p => p.Category)
               .HasForeignKey(p => p.CategoryId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new Category
            {
                Id = "a1e8c1f0-1f0c-4b77-9c7b-1d6b4a3c5e11",
                CategoryName = "Yazılım",
                CreatedDate = new DateTime(2025, 09, 14, 0, 0, 0, DateTimeKind.Utc),
                UpdatedDate = new DateTime(2025, 09, 14, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = "b2f9d2e1-2f1d-4c88-8d8c-2e7c5b4d6f22",
                CategoryName = "Teknoloji",
                CreatedDate = new DateTime(2025, 09, 14, 0, 0, 0, DateTimeKind.Utc),
                UpdatedDate = new DateTime(2025, 09, 14, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = "c3a0e3f2-3f2e-4d99-9e9d-3f8d6c7e8f33",
                CategoryName = ".Net",
                CreatedDate = new DateTime(2025, 09, 14, 0, 0, 0, DateTimeKind.Utc),
                UpdatedDate = new DateTime(2025, 09, 14, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
