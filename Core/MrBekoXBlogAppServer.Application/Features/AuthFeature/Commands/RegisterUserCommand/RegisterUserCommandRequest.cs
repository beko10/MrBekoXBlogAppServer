using MediatR;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.Commands.RegisterUserCommand;

public class RegisterUserCommandRequest : IRequest<RegisterUserCommandResponse>
{
    public RegisterUserCommandDto? registerUserCommandDtorequest { get; set; }
}
