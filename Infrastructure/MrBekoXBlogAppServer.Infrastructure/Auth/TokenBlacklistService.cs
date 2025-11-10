using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MrBekoXBlogAppServer.Application.Interfaces.Auth;

namespace MrBekoXBlogAppServer.Infrastructure.Auth;

public class TokenBlacklistService : ITokenBlacklistService
{
    private readonly IMemoryCache _cache;
    private readonly ILogger<TokenBlacklistService> _logger;

    public TokenBlacklistService(
        IMemoryCache cache,
        ILogger<TokenBlacklistService> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public Task BlacklistTokenAsync(string jti, DateTime expiresAt, CancellationToken cancellationToken = default)
    {
        var cacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpiration = expiresAt
        };

        _cache.Set($"blacklist:{jti}", true, cacheOptions);
        _logger.LogInformation("Token blacklisted: {Jti}", jti);

        return Task.CompletedTask;
    }

    public Task<bool> IsTokenBlacklistedAsync(string jti, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_cache.TryGetValue($"blacklist:{jti}", out _));
    }

    public Task CleanupExpiredTokensAsync(CancellationToken cancellationToken = default)
    {
        // MemoryCache otomatik temizler, ama Redis kullanıyorsanız manuel cleanup gerekebilir
        return Task.CompletedTask;
    }
}

