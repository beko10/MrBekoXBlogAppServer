using MediatR;
using MrBekoXBlogAppServer.Application.Features.SubCommentFeature.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Commands.UpdateSubCommentCommand;

public class UpdateSubCommentCommandRequest : IRequest<UpdateSubCommentCommandResponse>
{
    public UpdateSubCommentCommandDto? UpdateSubCommentCommandDtoRequest { get; set; }
}

