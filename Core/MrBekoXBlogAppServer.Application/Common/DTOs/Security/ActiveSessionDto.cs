namespace MrBekoXBlogAppServer.Application.Common.DTOs.Security;

public class ActiveSessionDto
{
    public string TokenHash { get; init; } = default!;
    public string? DeviceInfo { get; init; }
    public string? IpAddress { get; init; }
    public DateTime LastUsedAt { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime ExpiresAt { get; init; }
}

