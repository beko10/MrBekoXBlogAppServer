using MediatR;

namespace MrBekoXBlogAppServer.Application.Features.PostFeature.Commands.DeletePostCommand;

public class DeletePostCommandRequest : IRequest<DeletePostCommandResponse>
{
    public string? Id { get; set; }
}

