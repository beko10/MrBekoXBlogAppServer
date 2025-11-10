using MediatR;
using MrBekoXBlogAppServer.Application.Features.PostFeature.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.PostFeature.Commands.CreatePostCommand;

public class CreatePostCommandRequest : IRequest<CreatePostCommandResponse>
{
    public CreatePostCommandDto? CreatePostCommandDtoRequest { get; set; }
}

