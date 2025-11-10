using Microsoft.AspNetCore.Identity;

namespace MrBekoXBlogAppServer.Domain.Entities;

public class AppUser : IdentityUser
{
    public string FullName { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }

    //Navigation Properties
    public ICollection<Post> Posts { get; set; } = [];
    public ICollection<Comment> Comments { get; set; } = [];
    public ICollection<SubComment> SubComments { get; set; } = [];
}
