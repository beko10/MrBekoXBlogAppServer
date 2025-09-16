using MrBekoXBlogAppServer.Application.Common.CustomExceptions;
using System.Collections.Immutable;

namespace MrBekoXBlogAppServer.Application.Common.Results;

public class ResultData<TData> : Result
{
    public TData? Data { get; }

    private ResultData(bool isSuccess, string message, IReadOnlyList<AppException> errors, int statusCode, TData? data)
        : base(isSuccess, message, errors, statusCode)
    {
        Data = data;
    }

    public static ResultData<TData> Success(TData data, string message = DefaultSuccessMessage, int statusCode = 200)
        => new(true, message, ImmutableArray<AppException>.Empty, statusCode, data);

    public static new ResultData<TData> Failure(string message = DefaultFailureMessage, int statusCode = 400)
    => new(false, message, ImmutableArray<AppException>.Empty, statusCode, default);


    public static new ResultData<TData> Failure(AppException error, string message = DefaultFailureMessage, int statusCode = 400)
        => new(false, message, ImmutableArray.Create(error), statusCode, default);

    public static new ResultData<TData> Failure(IEnumerable<AppException> errors, string message = DefaultFailureMessage, int statusCode = 400)
        => new(false, message, errors.ToImmutableArray(), statusCode, default);

    public static implicit operator ResultData<TData>(TData data) => Success(data);
}