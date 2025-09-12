namespace MrBekoXBlogAppServer.Domain.Entities.Common;

public abstract class BaseEntity
{
    public string Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }

    protected BaseEntity()
    {
        Id = Guid.NewGuid().ToString();
        CreatedDate = DateTime.UtcNow;
        UpdatedDate = DateTime.UtcNow;
    }
}
