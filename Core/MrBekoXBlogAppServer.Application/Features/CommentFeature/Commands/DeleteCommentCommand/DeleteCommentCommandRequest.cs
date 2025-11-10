using MediatR;

namespace MrBekoXBlogAppServer.Application.Features.CommentFeature.Commands.DeleteCommentCommand;

public class DeleteCommentCommandRequest : IRequest<DeleteCommentCommandResponse>
{
    public string? Id { get; set; }
}

