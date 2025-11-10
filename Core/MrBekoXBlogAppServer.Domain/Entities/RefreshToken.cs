using MrBekoXBlogAppServer.Domain.Entities.Common;

namespace MrBekoXBlogAppServer.Domain.Entities;

public sealed class RefreshToken : BaseEntity
{
    public string UserId { get; set; } = default!;
    public string TokenHash { get; set; } = default!;
    public DateTime ExpiresAt { get; set; }
    public string? ReplacedByTokenHash { get; set; }
    public DateTime? RevokedAt { get; set; }
    public string? RevokedReason { get; set; }

    // Device Tracking
    public string? DeviceInfo { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public DateTime LastUsedAt { get; set; } = DateTime.UtcNow;

    public bool IsActive => RevokedAt is null && DateTime.UtcNow < ExpiresAt;
}
