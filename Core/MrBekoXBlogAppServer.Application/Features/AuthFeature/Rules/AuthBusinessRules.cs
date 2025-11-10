using Microsoft.AspNetCore.Identity;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Common.Security;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.Constants;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.RefreshTokenRepository;
using MrBekoXBlogAppServer.Domain.Entities;
using System.Net;
using System.Text.RegularExpressions;

namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.Rules;

public class AuthBusinessRules(
    UserManager<AppUser> userManager,
    IRefreshTokenReadRepository refreshTokenReadRepository
) : IAuthBusinessRules
{
    #region Common Rules

    public async Task<Result> UserMustExistAsync(string userId, CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
        {
            var exception = new BusinessRuleException(
                message: AuthBusinessRuleErrorMessages.UserNotFoundError
            );
            return Result.Failure(
                error: exception,
                message: AuthBusinessRuleMessages.UserNotFound,
                statusCode: (int)HttpStatusCode.NotFound
            );
        }

        return Result.Success();
    }

    public async Task<Result> UserMustExistByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user is null)
        {
            var exception = new BusinessRuleException(
                message: AuthBusinessRuleErrorMessages.UserNotFoundError
            );
            return Result.Failure(
                error: exception,
                message: AuthBusinessRuleMessages.UserNotFound,
                statusCode: (int)HttpStatusCode.NotFound
            );
        }

        return Result.Success();
    }

    public Task<Result> UserMustBeActiveAsync(AppUser user)
    {
        // Identity'nin LockoutEnabled ve LockoutEnd özellikleri ile kontrol
        if (user.LockoutEnabled && user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTimeOffset.UtcNow)
        {
            var exception = new BusinessRuleException(
                message: AuthBusinessRuleErrorMessages.UserNotActiveError
            );
            return Task.FromResult(Result.Failure(
                error: exception,
                message: AuthBusinessRuleMessages.UserNotActive,
                statusCode: (int)HttpStatusCode.Forbidden
            ));
        }

        return Task.FromResult(Result.Success());
    }

    #endregion

    #region Login Rules

    public Task<Result> EmailMustBeConfirmedAsync(AppUser user)
    {
        // Email confirmation gereksinimi varsa kontrol et
        if (!user.EmailConfirmed)
        {
            var exception = new BusinessRuleException(
                message: AuthBusinessRuleErrorMessages.EmailNotConfirmedError
            );
            return Task.FromResult(Result.Failure(
                error: exception,
                message: AuthBusinessRuleMessages.EmailNotConfirmed,
                statusCode: (int)HttpStatusCode.Forbidden
            ));
        }

        return Task.FromResult(Result.Success());
    }

    public async Task<Result> UserMustNotBeLockedOutAsync(AppUser user)
    {
        var isLockedOut = await userManager.IsLockedOutAsync(user);

        if (isLockedOut)
        {
            var exception = new BusinessRuleException(
                message: AuthBusinessRuleErrorMessages.UserLockedOutError
            );
            return Result.Failure(
                error: exception,
                message: AuthBusinessRuleMessages.UserLockedOut,
                statusCode: (int)HttpStatusCode.Forbidden
            );
        }

        return Result.Success();
    }

    public async Task<Result> PasswordMustBeValidAsync(AppUser user, string password)
    {
        var isValid = await userManager.CheckPasswordAsync(user, password);

        if (!isValid)
        {
            var exception = new BusinessRuleException(
                message: AuthBusinessRuleErrorMessages.InvalidPasswordError
            );
            return Result.Failure(
                error: exception,
                message: AuthBusinessRuleMessages.InvalidPassword,
                statusCode: (int)HttpStatusCode.Unauthorized
            );
        }

        return Result.Success();
    }

    public Task<Result> CheckLoginAttemptsAsync(AppUser user)
    {
        // Access failed count kontrolü
        if (user.AccessFailedCount >= 5)
        {
            var exception = new BusinessRuleException(
                message: AuthBusinessRuleErrorMessages.TooManyLoginAttemptsError
            );
            return Task.FromResult(Result.Failure(
                error: exception,
                message: AuthBusinessRuleMessages.TooManyLoginAttempts,
                statusCode: (int)HttpStatusCode.TooManyRequests
            ));
        }

        return Task.FromResult(Result.Success());
    }

    #endregion

    #region Register Rules

    public async Task<Result> EmailMustBeUniqueAsync(string email, CancellationToken cancellationToken = default)
    {
        var existingUser = await userManager.FindByEmailAsync(email);

        if (existingUser is not null)
        {
            var exception = new BusinessRuleException(
                message: AuthBusinessRuleErrorMessages.EmailAlreadyExistsError
            );
            return Result.Failure(
                error: exception,
                message: AuthBusinessRuleMessages.EmailAlreadyExists,
                statusCode: (int)HttpStatusCode.Conflict
            );
        }

        return Result.Success();
    }

    public async Task<Result> UsernameMustBeUniqueAsync(string username, CancellationToken cancellationToken = default)
    {
        var existingUser = await userManager.FindByNameAsync(username);

        if (existingUser is not null)
        {
            var exception = new BusinessRuleException(
                message: AuthBusinessRuleErrorMessages.UsernameAlreadyExistsError
            );
            return Result.Failure(
                error: exception,
                message: AuthBusinessRuleMessages.UsernameAlreadyExists,
                statusCode: (int)HttpStatusCode.Conflict
            );
        }

        return Result.Success();
    }

    public Result EmailFormatMustBeValid(string email)
    {
        // RFC 5322 standardına göre basit email validation
        var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);

        if (!emailRegex.IsMatch(email))
        {
            var exception = new BusinessRuleException(
                message: AuthBusinessRuleErrorMessages.InvalidEmailFormatError
            );
            return Result.Failure(
                error: exception,
                message: AuthBusinessRuleMessages.InvalidEmailFormat,
                statusCode: (int)HttpStatusCode.BadRequest
            );
        }

        return Result.Success();
    }

    public Result PasswordMustMeetRequirements(string password)
    {
        // Şifre politikası kontrolü
        // En az 8 karakter, 1 büyük harf, 1 küçük harf, 1 rakam, 1 özel karakter
        var hasMinimumLength = password.Length >= 8;
        var hasUpperCase = password.Any(char.IsUpper);
        var hasLowerCase = password.Any(char.IsLower);
        var hasDigit = password.Any(char.IsDigit);
        var hasSpecialChar = password.Any(c => !char.IsLetterOrDigit(c));

        if (!hasMinimumLength || !hasUpperCase || !hasLowerCase || !hasDigit || !hasSpecialChar)
        {
            var exception = new BusinessRuleException(
                message: AuthBusinessRuleErrorMessages.WeakPasswordError
            );
            return Result.Failure(
                error: exception,
                message: AuthBusinessRuleMessages.WeakPassword,
                statusCode: (int)HttpStatusCode.BadRequest
            );
        }

        return Result.Success();
    }

    public async Task<Result> UserCountMustBeUnderLimitAsync(int limit = 10000, CancellationToken cancellationToken = default)
    {
        var userCount = userManager.Users.Count();

        if (userCount >= limit)
        {
            var exception = new BusinessRuleException(
                message: AuthBusinessRuleErrorMessages.UserCountLimitExceededError
            );
            return Result.Failure(
                error: exception,
                message: AuthBusinessRuleMessages.UserCountLimitExceeded,
                statusCode: (int)HttpStatusCode.ServiceUnavailable
            );
        }

        return Result.Success();
    }

    #endregion

    #region RefreshToken Rules

    public async Task<Result> RefreshTokenMustBeValidAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var tokenHash = TokenHasher.HashToken(refreshToken);
        var token = await refreshTokenReadRepository.GetSingleAsync(
            x => x.TokenHash == tokenHash,
            tracking: false,
            cancellationToken: cancellationToken
        );

        if (token is null)
        {
            var exception = new BusinessRuleException(
                message: AuthBusinessRuleErrorMessages.InvalidRefreshTokenError
            );
            return Result.Failure(
                error: exception,
                message: AuthBusinessRuleMessages.InvalidRefreshToken,
                statusCode: (int)HttpStatusCode.Unauthorized
            );
        }

        return Result.Success();
    }

    public Result RefreshTokenMustNotBeExpired(RefreshToken refreshToken)
    {
        if (refreshToken.ExpiresAt <= DateTime.UtcNow)
        {
            var exception = new BusinessRuleException(
                message: AuthBusinessRuleErrorMessages.RefreshTokenExpiredError
            );
            return Result.Failure(
                error: exception,
                message: AuthBusinessRuleMessages.RefreshTokenExpired,
                statusCode: (int)HttpStatusCode.Unauthorized
            );
        }

        return Result.Success();
    }

    public Result RefreshTokenMustNotBeRevoked(RefreshToken refreshToken)
    {
        if (refreshToken.RevokedAt.HasValue)
        {
            var exception = new BusinessRuleException(
                message: AuthBusinessRuleErrorMessages.RefreshTokenRevokedError
            );
            return Result.Failure(
                error: exception,
                message: AuthBusinessRuleMessages.RefreshTokenRevoked,
                statusCode: (int)HttpStatusCode.Unauthorized
            );
        }

        return Result.Success();
    }

    #endregion

    #region Logout Rules

    public Result UserMustBeAuthenticated(string? userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            var exception = new BusinessRuleException(
                message: AuthBusinessRuleErrorMessages.UserMustBeAuthenticatedError
            );
            return Result.Failure(
                error: exception,
                message: AuthBusinessRuleMessages.UserMustBeAuthenticated,
                statusCode: (int)HttpStatusCode.Unauthorized
            );
        }

        return Result.Success();
    }

    #endregion
}
