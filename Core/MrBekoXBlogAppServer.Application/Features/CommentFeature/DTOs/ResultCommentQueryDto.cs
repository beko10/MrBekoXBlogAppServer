namespace MrBekoXBlogAppServer.Application.Features.CommentFeature.DTOs;

public class ResultCommentQueryDto
{
    public string Content { get; set; } = null!;
    public DateTime CommentDate { get; set; } = DateTime.Now;
}
