using System.Collections.Immutable;

namespace MrBekoXBlogAppServer.Application.Common.Results;

public class Result
{
    protected const string DefaultSuccessMessage = "Operation completed successfully.";
    protected const string DefaultFailureMessage = "Operation failed.";

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Message { get; }
    public int StatusCode { get; }
    public IReadOnlyList<string> Errors { get; }

    protected Result(bool isSuccess, string message, IReadOnlyList<string> errors, int statusCode)
    {
        IsSuccess = isSuccess;
        Message = message ?? throw new ArgumentNullException(nameof(message));
        Errors = errors ?? throw new ArgumentNullException(nameof(errors));
        StatusCode = statusCode;
    }

    public static Result Success(string message = DefaultSuccessMessage, int statusCode = 200)
        => new(true, message, ImmutableArray<string>.Empty, statusCode);

    public static Result Failure(string message = DefaultFailureMessage, int statusCode = 400)
    => new(false,message, ImmutableArray<string>.Empty, statusCode);


    public static Result Failure(string error, string message = DefaultFailureMessage, int statusCode = 400)
        => new(false, message, ImmutableArray.Create(error), statusCode);

    public static Result Failure(IEnumerable<string> errors, string message = DefaultFailureMessage, int statusCode = 400)
        => new(false, message, errors.ToImmutableArray(), statusCode);

    
    public static implicit operator bool(Result result) => result.IsSuccess;
}