using MrBekoXBlogAppServer.Domain.Entities.Common;

namespace MrBekoXBlogAppServer.Domain.Entities;

public class Comment : BaseEntity
{
    public string Content { get; set; }
    public DateTime CommentDate { get; set; }

    //Navigation Properties
    public string PostId { get; set; } = null!;
    public Post? Post { get; set; }
    public string UserId { get; set; } = null!;
    public AppUser? User { get; set; }
    public ICollection<SubComment> SubComments { get; set; } = [];
}
