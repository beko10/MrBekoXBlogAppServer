using MrBekoXBlogAppServer.Domain.Entities.Common;

namespace MrBekoXBlogAppServer.Domain.Entities;

public sealed class Category : BaseEntity
{
    public string CategoryName { get; set; } = null!;
    public IEnumerable<Post> Posts { get; set; } = [];

}
