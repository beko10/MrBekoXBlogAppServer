namespace MrBekoXBlogAppServer.Application.Features.SubCommentFeature.DTOs;

public class ResultSubCommentQueryDto
{
    public string Id { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime CommentDate { get; set; }
    public string CommentId { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}

