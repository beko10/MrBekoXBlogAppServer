using MediatR;
using MrBekoXBlogAppServer.Application.Features.CommentFeature.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.CommentFeature.Commands.CreateCommentCommand;

public class CreateCommentCommandRequest : IRequest<CreateCommentCommandResponse>
{
    public CreateCommentCommandDto? CreateCommentCommandDtoRequest { get; set; }
}

