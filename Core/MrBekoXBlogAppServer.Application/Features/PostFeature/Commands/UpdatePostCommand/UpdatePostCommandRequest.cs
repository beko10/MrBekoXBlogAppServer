using MediatR;
using MrBekoXBlogAppServer.Application.Features.PostFeature.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.PostFeature.Commands.UpdatePostCommand;

public class UpdatePostCommandRequest : IRequest<UpdatePostCommandResponse>
{
    public UpdatePostCommandDto? UpdatePostCommandDtoRequest { get; set; }
}

