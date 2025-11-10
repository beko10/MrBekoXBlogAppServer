using MediatR;

namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.Commands.LogoutUserCommand;

public class LogoutUserCommandRequest : IRequest<LogoutUserCommandResponse>
{
    public string RefreshToken { get; set; } = string.Empty;
    public string? Jti { get; set; }
    public string? UserId { get; set; } // Business rules için gerekli
}

