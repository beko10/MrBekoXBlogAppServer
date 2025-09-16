using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MrBekoXBlogAppServer.Domain.Entities;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("Messages");
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Name).IsRequired().HasMaxLength(100);
        builder.Property(m => m.Email).IsRequired().HasMaxLength(150);
        builder.Property(m => m.Subject).IsRequired().HasMaxLength(200);
        builder.Property(m => m.Content).IsRequired();

        builder.HasData(
            new Message
            {
                Id = "1a9d8ffd-7d66-44ed-bfb7-232720a18230",
                Name = "Ömer Faruk Doğan",
                Email = "omerfarukdogan@example.com",
                Subject = "Merhaba",
                Content = "Sitenizi çok beğendim!",
                IsRead = false,
                CreatedDate = new DateTime(2025, 09, 14, 0, 0, 0, DateTimeKind.Utc),
                UpdatedDate = new DateTime(2025, 09, 14, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
