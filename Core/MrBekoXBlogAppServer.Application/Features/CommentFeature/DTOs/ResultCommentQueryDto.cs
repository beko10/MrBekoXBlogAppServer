namespace MrBekoXBlogAppServer.Application.Features.CommentFeature.DTOs;

public class ResultCommentQueryDto
{
    public string Id { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime CommentDate { get; set; }
    public string PostId { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
