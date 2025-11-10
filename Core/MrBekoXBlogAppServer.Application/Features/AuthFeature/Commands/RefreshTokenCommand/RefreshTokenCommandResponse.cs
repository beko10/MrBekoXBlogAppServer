using MediatR;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Common.DTOs.Security;

namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.Commands.RefreshTokenCommand;

public class RefreshTokenCommandResponse
{
    public ResultData<TokenDto> Result { get; set; } = null!;
}

