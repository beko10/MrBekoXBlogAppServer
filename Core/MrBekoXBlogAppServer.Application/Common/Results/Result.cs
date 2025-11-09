using MrBekoXBlogAppServer.Application.Common.CustomExceptions;
using System.Collections.Immutable;

namespace MrBekoXBlogAppServer.Application.Common.Results;

public class Result
{
    protected const string DefaultSuccessMessage = "Operation completed successfully.";
    protected const string DefaultFailureMessage = "Operation failed.";

    public bool IsSuccess { get; init; }
    public bool IsFailure => !IsSuccess;
    public string Message { get; init; }
    public int StatusCode { get; init; }
    public IReadOnlyList<AppException> Errors { get; init; }

    protected Result(bool isSuccess, string message, IReadOnlyList<AppException> errors, int statusCode)
    {
        IsSuccess = isSuccess;
        Message = message ?? throw new ArgumentNullException(nameof(message));
        Errors = errors ?? throw new ArgumentNullException(nameof(errors));
        StatusCode = statusCode;
    }

    public static Result Success(string message = DefaultSuccessMessage, int statusCode = 200)
        => new(true, message, ImmutableArray<AppException>.Empty, statusCode);

    public static Result Failure(string message = DefaultFailureMessage, int statusCode = 400)
    => new(false,message, ImmutableArray<AppException>.Empty, statusCode);


    public static Result Failure(AppException error, string message = DefaultFailureMessage, int statusCode = 400)
        => new(false, message, ImmutableArray.Create(error), statusCode);

    public static Result Failure(IEnumerable<AppException> errors, string message = DefaultFailureMessage, int statusCode = 400)
        => new(false, message, errors.ToImmutableArray(), statusCode);



    public static implicit operator bool(Result result) => result.IsSuccess;
}