using MrBekoXBlogAppServer.Domain.Entities.Common;

namespace MrBekoXBlogAppServer.Domain.Entities;

public class ContactInfo : BaseEntity
{
    public string Address { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string MapUrl { get; set; } = null!;
}
