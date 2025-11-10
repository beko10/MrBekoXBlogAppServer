using MediatR;
using MrBekoXBlogAppServer.Application.Features.SubCommentFeature.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Commands.CreateSubCommentCommand;

public class CreateSubCommentCommandRequest : IRequest<CreateSubCommentCommandResponse>
{
    public CreateSubCommentCommandDto? CreateSubCommentCommandDtoRequest { get; set; }
}

