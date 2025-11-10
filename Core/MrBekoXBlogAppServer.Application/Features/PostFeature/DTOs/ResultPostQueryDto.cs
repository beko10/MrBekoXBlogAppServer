namespace MrBekoXBlogAppServer.Application.Features.PostFeature.DTOs;

public class ResultPostQueryDto
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string CoverImageUrl { get; set; } = null!;
    public string PostImageUrl { get; set; } = null!;
    public DateTime PublishedDate { get; set; }
    public string CategoryId { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}

