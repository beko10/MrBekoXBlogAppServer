using MrBekoXBlogAppServer.Domain.Entities.Common;

namespace MrBekoXBlogAppServer.Domain.Entities;

public sealed class Message : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Content { get; set; } = null!;
    public bool IsRead { get; set; } = false;
}
