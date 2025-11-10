using MediatR;

namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.Commands.LoginUserCommand;

public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}
