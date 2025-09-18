using MrBekoXBlogAppServer.Domain.Entities.Common;
using System.Security.Cryptography;

namespace MrBekoXBlogAppServer.Domain.Entities;

public sealed class RefreshToken : BaseEntity
{ 
    public string UserId { get; set; } = default!;
    public string TokenHash { get; set; } = default!;
    public DateTime ExpiresAt { get; set; }
    public string? ReplacedByTokenHash { get; set; }
    public DateTime? RevokedAt { get; set; }
    public string? RevokedReason { get; set; }
    public bool IsActive => RevokedAt is null && DateTime.UtcNow < ExpiresAt;
}
