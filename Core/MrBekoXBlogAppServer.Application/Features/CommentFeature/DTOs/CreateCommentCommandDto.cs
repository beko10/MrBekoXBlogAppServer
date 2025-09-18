namespace MrBekoXBlogAppServer.Application.Features.CommentFeature.DTOs;

public class CreateCommentCommandDto
{
    public string? Content { get; set; }
    public string PostId { get; set; } = null!;

}
