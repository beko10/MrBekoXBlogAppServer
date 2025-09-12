using MrBekoXBlogAppServer.Domain.Entities.Common;

namespace MrBekoXBlogAppServer.Domain.Entities;

public sealed class Post : BaseEntity
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string CoverImageUrl { get; set; } = null!;
    public string PostImageUrl { get; set; } = null!;
    public DateTime PublishedDate { get; set; }
    public string CategoryId { get; set; } = null!;
    public Category Category { get; set; } = null!;
}
