using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MrBekoXBlogAppServer.Domain.Entities;

namespace MrBekoXBlogAppServer.Persistence.Configuration;

public class SocialMediaConfiguration : IEntityTypeConfiguration<SocialMedia>
{
    public void Configure(EntityTypeBuilder<SocialMedia> builder)
    {
        builder.ToTable("SocialMedias");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Name).IsRequired().HasMaxLength(50);
        builder.Property(s => s.Url).IsRequired().HasMaxLength(300);
        builder.Property(s => s.Icon).HasMaxLength(100);
        builder.HasData(
        new SocialMedia
        {
            Id = "09b72f9e-d1a2-40b4-802e-f81b3b2642ed",
            Name = "GitHub",
            Url = "https://github.com/username",
            Icon = "fa-github",
            CreatedDate = new DateTime(2025, 09, 13, 0, 0, 0, DateTimeKind.Utc), 
            UpdatedDate = new DateTime(2025, 09, 13, 0, 0, 0, DateTimeKind.Utc)  
        },
        new SocialMedia
        {
            Id = "740a685e-fe6e-4273-bf48-bfc517d5e8d2",
            Name = "LinkedIn",
            Url = "https://linkedin.com/in/username",
            Icon = "fa-linkedin",
            CreatedDate = new DateTime(2025, 09, 13, 0, 0, 0, DateTimeKind.Utc),
            UpdatedDate = new DateTime(2025, 09, 13, 0, 0, 0, DateTimeKind.Utc)
        }
        );
    }
}
