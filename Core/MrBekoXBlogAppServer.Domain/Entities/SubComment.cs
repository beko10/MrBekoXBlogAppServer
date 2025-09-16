using MrBekoXBlogAppServer.Domain.Entities.Common;

namespace MrBekoXBlogAppServer.Domain.Entities;

public class SubComment : BaseEntity
{
    public string Content { get; set; }
    public DateTime CommentDate { get; set; }

    //Navigation Properties
    public string CommentId { get; set; } = null!;
    public Comment? Comment { get; set; }
    public string UserId { get; set; } = null!;
    public AppUser? User { get; set; }
}
