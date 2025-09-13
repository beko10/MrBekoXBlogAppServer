using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MrBekoXBlogAppServer.Domain.Entities;

namespace MrBekoXBlogAppServer.Persistence.Configuration;

public class ContactInfoConfiguration : IEntityTypeConfiguration<ContactInfo>
{
    public void Configure(EntityTypeBuilder<ContactInfo> builder)
    {
        builder.ToTable("ContactInfos");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Address).IsRequired().HasMaxLength(250);
        builder.Property(c => c.Email).IsRequired().HasMaxLength(150);
        builder.Property(c => c.Phone).IsRequired().HasMaxLength(20);
        builder.Property(c => c.MapUrl).HasMaxLength(500);
        builder.HasData(
            new ContactInfo
            {
                Id = "4ff33380-5726-4d98-ae0f-c3b3e0017928",
                Address = "İstanbul, Türkiye",
                Email = "info@blogapp.com",
                Phone = "+90 555 555 55 55",
                MapUrl = "https://maps.google.com/example",
                CreatedDate = new DateTime(2025, 09, 13, 0, 0, 0, DateTimeKind.Utc),
                UpdatedDate = new DateTime(2025, 09, 13, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}