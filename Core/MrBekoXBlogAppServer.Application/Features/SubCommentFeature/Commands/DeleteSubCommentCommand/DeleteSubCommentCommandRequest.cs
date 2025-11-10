using MediatR;

namespace MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Commands.DeleteSubCommentCommand;

public class DeleteSubCommentCommandRequest : IRequest<DeleteSubCommentCommandResponse>
{
    public string? Id { get; set; }
}

