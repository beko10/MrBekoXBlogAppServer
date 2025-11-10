using MediatR;
using MrBekoXBlogAppServer.Application.Common.Results;

namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.Commands.LogoutUserCommand;

public class LogoutUserCommandResponse
{
    public Result Result { get; set; } = null!;
}

