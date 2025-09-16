using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MrBekoXBlogAppServer.Domain.Entities;

public class SubCommentConfiguration : IEntityTypeConfiguration<SubComment>
{
    public void Configure(EntityTypeBuilder<SubComment> builder)
    {
        builder.ToTable("SubComments");
        builder.HasKey(sc => sc.Id);

        builder.Property(sc => sc.Content).IsRequired();
        builder.Property(sc => sc.CommentDate).IsRequired();

        // SubComment → Comment (Cascade)
        builder.HasOne(sc => sc.Comment)
               .WithMany(c => c.SubComments)
               .HasForeignKey(sc => sc.CommentId)
               .OnDelete(DeleteBehavior.Cascade);

        // SubComment → User (Restrict)
        builder.HasOne(sc => sc.User)
               .WithMany(u => u.SubComments)
               .HasForeignKey(sc => sc.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new SubComment
            {
                Id = "s1e8d2f0-3f0c-4a77-9c7b-1d6b4a3c5e33",
                Content = "Ben de katılıyorum!",
                CommentDate = new DateTime(2025, 09, 14, 0, 0, 0, DateTimeKind.Utc),
                CommentId = "c1e8d2f0-2f0c-4a77-9c7b-1d6b4a3c5e22",
                UserId = "u1234567-89ab-cdef-0123-456789abcdef",
                CreatedDate = new DateTime(2025, 09, 14, 0, 0, 0, DateTimeKind.Utc),
                UpdatedDate = new DateTime(2025, 09, 14, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
