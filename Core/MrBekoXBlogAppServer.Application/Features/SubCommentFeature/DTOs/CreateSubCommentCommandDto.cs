namespace MrBekoXBlogAppServer.Application.Features.SubCommentFeature.DTOs;

public class CreateSubCommentCommandDto
{
    public string Content { get; set; } = null!;
    public string CommentId { get; set; } = null!;
}

