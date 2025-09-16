using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MrBekoXBlogAppServer.Domain.Entities;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Content)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(c => c.CommentDate).IsRequired();

        // Comment → Post (Cascade)
        builder.HasOne(c => c.Post)
               .WithMany(p => p.Comments)
               .HasForeignKey(c => c.PostId)
               .OnDelete(DeleteBehavior.Cascade);

        // Comment → User (Restrict)
        builder.HasOne(c => c.User)
               .WithMany(u => u.Comments)
               .HasForeignKey(c => c.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new Comment
            {
                Id = "c1e8d2f0-2f0c-4a77-9c7b-1d6b4a3c5e22",
                Content = "Çok faydalı bir yazı olmuş!",
                CommentDate = new DateTime(2025, 09, 14, 0, 0, 0, DateTimeKind.Utc),
                PostId = "30b81e45-a012-440d-bf6f-1a56a2e3d50f",
                UserId = "u1234567-89ab-cdef-0123-456789abcdef",
                CreatedDate = new DateTime(2025, 09, 14, 0, 0, 0, DateTimeKind.Utc),
                UpdatedDate = new DateTime(2025, 09, 14, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
