namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.Constants;

/// <summary>
/// Auth business rule hata mesajları (teknik detaylar için)
/// </summary>
public static class AuthBusinessRuleErrorMessages
{
    #region Login Rules

    public const string UserNotFoundError = "AUTH_USER_NOT_FOUND";
    public const string UserNotActiveError = "AUTH_USER_NOT_ACTIVE";
    public const string EmailNotConfirmedError = "AUTH_EMAIL_NOT_CONFIRMED";
    public const string UserLockedOutError = "AUTH_USER_LOCKED_OUT";
    public const string InvalidPasswordError = "AUTH_INVALID_PASSWORD";
    public const string TooManyLoginAttemptsError = "AUTH_TOO_MANY_LOGIN_ATTEMPTS";

    #endregion

    #region Register Rules

    public const string EmailAlreadyExistsError = "AUTH_EMAIL_ALREADY_EXISTS";
    public const string UsernameAlreadyExistsError = "AUTH_USERNAME_ALREADY_EXISTS";
    public const string InvalidEmailFormatError = "AUTH_INVALID_EMAIL_FORMAT";
    public const string WeakPasswordError = "AUTH_WEAK_PASSWORD";
    public const string UserCountLimitExceededError = "AUTH_USER_COUNT_LIMIT_EXCEEDED";
    public const string InvalidAgeError = "AUTH_INVALID_AGE";

    #endregion

    #region RefreshToken Rules

    public const string InvalidRefreshTokenError = "AUTH_INVALID_REFRESH_TOKEN";
    public const string RefreshTokenExpiredError = "AUTH_REFRESH_TOKEN_EXPIRED";
    public const string RefreshTokenRevokedError = "AUTH_REFRESH_TOKEN_REVOKED";
    public const string UserNoLongerExistsError = "AUTH_USER_NO_LONGER_EXISTS";
    public const string UserNoLongerActiveError = "AUTH_USER_NO_LONGER_ACTIVE";
    public const string TokenReuseDetectedError = "AUTH_TOKEN_REUSE_DETECTED";

    #endregion

    #region Logout Rules

    public const string UserMustBeAuthenticatedError = "AUTH_USER_MUST_BE_AUTHENTICATED";
    public const string InvalidTokenError = "AUTH_INVALID_TOKEN";

    #endregion

    #region Common Rules

    public const string UserMustExistError = "AUTH_USER_MUST_EXIST";
    public const string UserMustBeActiveError = "AUTH_USER_MUST_BE_ACTIVE";

    #endregion
}
