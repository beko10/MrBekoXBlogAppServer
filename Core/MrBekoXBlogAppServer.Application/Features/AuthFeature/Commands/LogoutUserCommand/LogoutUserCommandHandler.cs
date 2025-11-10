using MediatR;
using MrBekoXBlogAppServer.Application.Common.BusinessRuleEngines;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.Rules;
using MrBekoXBlogAppServer.Application.Interfaces.Auth;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.RefreshTokenRepository;
using MrBekoXBlogAppServer.Application.Interfaces.UnitOfWorks;
using System.Security.Cryptography;

namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.Commands.LogoutUserCommand;

public class LogoutUserCommandHandler(
    IRefreshTokenReadRepository refreshTokenReadRepository,
    ITokenBlacklistService tokenBlacklistService,
    IUnitOfWork unitOfWork,
    IAuthBusinessRules authBusinessRules
) : IRequestHandler<LogoutUserCommandRequest, LogoutUserCommandResponse>
{
    public async Task<LogoutUserCommandResponse> Handle(
        LogoutUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        // 1. Business Rules: Run logout validation rules using BusinessRuleEngine
        var ruleResult = await BusinessRuleEngine.RunAsync(
            () => Task.FromResult(authBusinessRules.UserMustBeAuthenticated(request.UserId))
        );

        if (ruleResult.IsFailure)
        {
            return new LogoutUserCommandResponse
            {
                Result = ruleResult
            };
        }

        // 2. Refresh token'ı iptal et
        var tokenHash = HashToken(request.RefreshToken);
        var refreshToken = await refreshTokenReadRepository.GetSingleAsync(
            x => x.TokenHash == tokenHash && x.IsActive,
            tracking: true,
            cancellationToken: cancellationToken);

        if (refreshToken != null)
        {
            refreshToken.RevokedAt = DateTime.UtcNow;
            refreshToken.RevokedReason = "User logout";
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        // Access token'ı blacklist'e ekle
        if (!string.IsNullOrEmpty(request.Jti))
        {
            await tokenBlacklistService.BlacklistTokenAsync(
                request.Jti,
                DateTime.UtcNow.AddMinutes(60), // Access token süresi kadar
                cancellationToken);
        }

        return new LogoutUserCommandResponse
        {
            Result = Result.Success(AuthOperationResultMessages.LogoutSuccess, 200)
        };
    }

    private static string HashToken(string token)
    {
        using var sha256 = SHA256.Create();
        var tokenBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(token));
        return Convert.ToBase64String(tokenBytes);
    }
}

