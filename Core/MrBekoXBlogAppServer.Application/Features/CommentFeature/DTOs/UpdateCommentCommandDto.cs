namespace MrBekoXBlogAppServer.Application.Features.CommentFeature.DTOs;

public class UpdateCommentCommandDto
{
    public string Id { get; set; } = null!;
    public string Content { get; set; } = null!;
}
