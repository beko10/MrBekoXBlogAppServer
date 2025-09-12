using MrBekoXBlogAppServer.Domain.Entities.Common;

namespace MrBekoXBlogAppServer.Domain.Entities;

public sealed class SocialMedia: BaseEntity
{
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string Icon { get; set; } = null!;
}
