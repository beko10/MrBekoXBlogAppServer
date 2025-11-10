using Microsoft.EntityFrameworkCore;
using MrBekoXBlogAppServer.Application.Common.DTOs.Security;
using MrBekoXBlogAppServer.Application.Interfaces.Auth;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.RefreshTokenRepository;
using MrBekoXBlogAppServer.Application.Interfaces.UnitOfWorks;

namespace MrBekoXBlogAppServer.Infrastructure.Auth;

public class SessionManagementService : ISessionManagementService
{
    private readonly IRefreshTokenReadRepository _readRepository;
    private readonly IRefreshTokenWriteRepository _writeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SessionManagementService(
        IRefreshTokenReadRepository readRepository,
        IRefreshTokenWriteRepository writeRepository,
        IUnitOfWork unitOfWork)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ActiveSessionDto>> GetActiveSessionsAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        var query = _readRepository.GetWhere(
            x => x.UserId == userId && x.IsActive,
            tracking: false,
            cancellationToken: cancellationToken);

        var tokens = await query.ToListAsync(cancellationToken);

        return tokens.Select(t => new ActiveSessionDto
        {
            TokenHash = t.TokenHash,
            DeviceInfo = t.DeviceInfo,
            IpAddress = t.IpAddress,
            LastUsedAt = t.LastUsedAt,
            CreatedAt = t.CreatedDate,
            ExpiresAt = t.ExpiresAt
        }).ToList();
    }

    public async Task<bool> CanCreateNewSessionAsync(
        string userId,
        int maxConcurrentSessions = 5,
        CancellationToken cancellationToken = default)
    {
        var activeSessions = await _readRepository.CountWhereAsync(
            x => x.UserId == userId && x.IsActive,
            cancellationToken);

        return activeSessions < maxConcurrentSessions;
    }

    public async Task RevokeSessionAsync(
        string userId,
        string refreshTokenHash,
        CancellationToken cancellationToken = default)
    {
        var token = await _readRepository.GetSingleAsync(
            x => x.UserId == userId && x.TokenHash == refreshTokenHash && x.IsActive,
            tracking: true,
            cancellationToken: cancellationToken);

        if (token != null)
        {
            token.RevokedAt = DateTime.UtcNow;
            token.RevokedReason = "User revoked";
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task RevokeAllSessionsAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        await _writeRepository.RevokeAllAsync(userId, "User revoked all sessions", cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

