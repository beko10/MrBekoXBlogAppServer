namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.DTOs.LoginUserCommandDTOs;

public class UserDto
{
    public string UserId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = [];
}
