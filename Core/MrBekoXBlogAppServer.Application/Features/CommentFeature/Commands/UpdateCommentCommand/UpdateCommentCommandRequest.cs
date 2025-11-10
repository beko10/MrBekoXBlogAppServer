using MediatR;
using MrBekoXBlogAppServer.Application.Features.CommentFeature.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.CommentFeature.Commands.UpdateCommentCommand;

public class UpdateCommentCommandRequest : IRequest<UpdateCommentCommandResponse>
{
    public UpdateCommentCommandDto? UpdateCommentCommandDtoRequest { get; set; }
}

