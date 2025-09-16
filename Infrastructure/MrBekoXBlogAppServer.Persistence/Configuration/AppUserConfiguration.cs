using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MrBekoXBlogAppServer.Domain.Entities;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable("AspNetUsers");
        builder.Property(u => u.FullName).IsRequired().HasMaxLength(150);
        builder.Property(u => u.ImageUrl).HasMaxLength(300);

        builder.HasData(
            new AppUser
            {
                Id = "u1234567-89ab-cdef-0123-456789abcdef",
                UserName = "berkay",
                NormalizedUserName = "BERKAY",
                Email = "berkay@example.com",
                NormalizedEmail = "BERKAY@EXAMPLE.COM",
                FullName = "Berkay Beko",
                ImageUrl = "/images/users/berkay.jpg",
                // Static DateTime - DateTimeKind.Utc yerine new DateTime kullan
                CreatedDate = new DateTime(2025, 9, 14),
                // Static hash - "Mr_Beko.4" için pre-generated
                PasswordHash = "AQAAAAIAAYagAAAAEHfk2/3QXmQzYFJ4vGm8N9p7UgLJJ2cR4Ke9Z5xP6oK3w8T1sY2nH7qV5bF9jL4mE3==",
                SecurityStamp = "SEED-SECURITY-STAMP-123456789",
                ConcurrencyStamp = "SEED-CONCURRENCY-STAMP-123456789"
            }
        );
    }
}