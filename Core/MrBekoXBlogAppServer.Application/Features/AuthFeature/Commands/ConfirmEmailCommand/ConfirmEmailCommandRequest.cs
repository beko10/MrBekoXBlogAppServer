using MediatR;

namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.Commands.ConfirmEmailCommand;

public class ConfirmEmailCommandRequest : IRequest<ConfirmEmailCommandResponse>
{
    public string? UserId { get; set; }
    public string? Token { get; set; }
}
