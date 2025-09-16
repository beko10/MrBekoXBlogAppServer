namespace MrBekoXBlogAppServer.Application.Features.ContactInfoFeature.DTOs;

public class CreateContactInfoCommandDto
{
    public string Address { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string MapUrl { get; set; } = null!;
}
