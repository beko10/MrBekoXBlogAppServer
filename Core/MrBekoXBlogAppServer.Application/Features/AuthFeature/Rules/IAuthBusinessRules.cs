using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Domain.Entities;

namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.Rules;

public interface IAuthBusinessRules
{
    #region Common Rules

    Task<Result> UserMustExistAsync(string userId, CancellationToken cancellationToken = default);
    Task<Result> UserMustExistByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<Result> UserMustBeActiveAsync(AppUser user);

    #endregion

    #region Login Rules

    Task<Result> EmailMustBeConfirmedAsync(AppUser user);
    Task<Result> UserMustNotBeLockedOutAsync(AppUser user);
    Task<Result> PasswordMustBeValidAsync(AppUser user, string password);
    Task<Result> CheckLoginAttemptsAsync(AppUser user);

    #endregion

    #region Register Rules

    Task<Result> EmailMustBeUniqueAsync(string email, CancellationToken cancellationToken = default);
    Task<Result> UsernameMustBeUniqueAsync(string username, CancellationToken cancellationToken = default);
    Result EmailFormatMustBeValid(string email);
    Result PasswordMustMeetRequirements(string password);
    Task<Result> UserCountMustBeUnderLimitAsync(int limit = 10000, CancellationToken cancellationToken = default);

    #endregion

    #region RefreshToken Rules

    Task<Result> RefreshTokenMustBeValidAsync(string refreshToken, CancellationToken cancellationToken = default);
    Result RefreshTokenMustNotBeExpired(RefreshToken refreshToken);
    Result RefreshTokenMustNotBeRevoked(RefreshToken refreshToken);

    #endregion

    #region Logout Rules

    Result UserMustBeAuthenticated(string? userId);

    #endregion
}
