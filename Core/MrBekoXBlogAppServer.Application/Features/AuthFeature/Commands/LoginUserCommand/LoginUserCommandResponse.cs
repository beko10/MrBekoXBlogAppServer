using MediatR;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.DTOs.LoginUserCommandDTOs;

namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.Commands.LoginUserCommand;

public class LoginUserCommandResponse
{
    public ResultData<LoginUserCommandResponseDto> Result { get; set; } = null!;
}

public class LoginUserCommandResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
    public DateTime AccessTokenExpiresAt { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenExpiresAt { get; set; }
    public string Jti { get; set; } = string.Empty;
    public UserDto UserDto { get; set; } = null!;
    public string DeviceInfo { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public string Message { get; set; } = "Login successful";
}
