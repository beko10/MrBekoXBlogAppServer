namespace MrBekoXBlogAppServer.Application.Features.PostFeature.DTOs;

public class CreatePostCommandDto
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string CoverImageUrl { get; set; } = null!;
    public string PostImageUrl { get; set; } = null!;
    public string CategoryId { get; set; } = null!;
}

