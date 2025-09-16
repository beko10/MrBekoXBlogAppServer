namespace MrBekoXBlogAppServer.Application.Features.ContactInfoFeature.DTOs;

public class ResultContactInfoQueryDto
{
    public string Address { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string MapUrl { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
