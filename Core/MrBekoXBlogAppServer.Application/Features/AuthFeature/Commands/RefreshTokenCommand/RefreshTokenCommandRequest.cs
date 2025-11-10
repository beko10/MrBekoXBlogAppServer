using MediatR;

namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.Commands.RefreshTokenCommand;

public class RefreshTokenCommandRequest : IRequest<RefreshTokenCommandResponse>
{
    public string RefreshToken { get; set; } = string.Empty;
}

