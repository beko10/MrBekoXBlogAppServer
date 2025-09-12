using MrBekoXBlogAppServer.Application.Common.CustomExceptions;
using System.Text.Json;

namespace MrBekoXBlogAppServer.API.Middleware;


public sealed class ExceptionHandlingMiddleware(
    ILogger<ExceptionHandlingMiddleware> logger,
    IWebHostEnvironment environment) : IMiddleware
{

    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;
    private readonly IWebHostEnvironment _environment = environment;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // 3. Enhanced pattern matching (.NET 9)
        var problemDetails = exception switch
        {
            ValidationException validationEx => CreateProblemDetails(
                context, 400, validationEx.Message, "VALIDATION_FAILED", validationEx.Errors),

            NotFoundException notFoundEx => CreateProblemDetails(
                context, 404, notFoundEx.Message, "ENTITY_NOT_FOUND"),

            BusinessRuleException businessEx => CreateProblemDetails(
                context, 422, businessEx.Message, "BUSINESS_RULE_VIOLATION"),

            UnauthorizedAccessException => CreateProblemDetails(
                context, 401, "Access denied", "UNAUTHORIZED"),

            ArgumentException => CreateProblemDetails(
                context, 400, "Invalid input parameters", "INVALID_ARGUMENT"),

            InvalidOperationException => CreateProblemDetails(
                context, 500, "Invalid operation state", "INVALID_OPERATION"),

            TimeoutException => CreateProblemDetails(
                context, 408, "The operation has timed out", "OPERATION_TIMEOUT"),

            TaskCanceledException { InnerException: TimeoutException } => CreateProblemDetails(
                context, 408, "Request timeout", "REQUEST_TIMEOUT"),

            TaskCanceledException => CreateProblemDetails(
                context, 499, "Request was canceled", "REQUEST_CANCELED"),

            HttpRequestException => CreateProblemDetails(
                context, 502, "External service unavailable", "EXTERNAL_SERVICE_ERROR"),

            NotImplementedException => CreateProblemDetails(
                context, 501, "Feature not implemented", "NOT_IMPLEMENTED"),

            NotSupportedException => CreateProblemDetails(
                context, 400, "Operation not supported", "NOT_SUPPORTED"),

            InvalidDataException => CreateProblemDetails(
                context, 422, "Data integrity violation", "DATA_INTEGRITY_ERROR"),

            OutOfMemoryException or StackOverflowException
            or ThreadAbortException or AccessViolationException => throw exception,

            AppException appEx => CreateProblemDetails(
                context, appEx.StatusCode, appEx.Message, appEx.ErrorCode),


            _ => CreateProblemDetails(context, 500,
                _environment.IsDevelopment() ? $"Internal server error: {exception.Message}"
                                            : "An internal server error occurred",
                "INTERNAL_SERVER_ERROR")
        };

        LogException(context, exception, problemDetails.Status);

        await WriteResponseAsync(context, problemDetails);
    }

    private record ProblemDetailsResponse(
        string Type,
        string Title,
        int Status,
        string Detail,
        string ErrorCode,
        string TraceId,
        DateTime Timestamp,
        string? Path,
        string[]? Errors = null);

    private ProblemDetailsResponse CreateProblemDetails(
        HttpContext context,
        int status,
        string message,
        string errorCode,
        IEnumerable<string>? errors = null)
    {
        return new ProblemDetailsResponse(
            Type: $"https://httpstatuses.com/{status}",
            Title: GetStatusTitle(status),
            Status: status,
            Detail: message,
            ErrorCode: errorCode,
            TraceId: context.TraceIdentifier,
            Timestamp: DateTime.UtcNow,
            Path: context.Request.Path.Value,
            Errors: errors?.Where(e => !string.IsNullOrWhiteSpace(e)).ToArray()
        );
    }

    private static async Task WriteResponseAsync(HttpContext context, ProblemDetailsResponse problem)
    {
        context.Response.StatusCode = problem.Status;
        context.Response.ContentType = "application/json";

       
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        await context.Response.WriteAsJsonAsync(problem, options);
    }

    private void LogException(HttpContext context, Exception exception, int statusCode)
    {
        var logLevel = statusCode switch
        {
            >= 500 => LogLevel.Error,
            >= 400 => LogLevel.Warning,
            _ => LogLevel.Information
        };

        _logger.Log(logLevel, exception,
            "Exception handled: {ExceptionType} | Path: {Path} | StatusCode: {StatusCode} | TraceId: {TraceId}",
            exception.GetType().Name,
            context.Request.Path,
            statusCode,
            context.TraceIdentifier);
    }

    private static string GetStatusTitle(int statusCode) => statusCode switch
    {
        400 => "Bad Request",
        401 => "Unauthorized",
        404 => "Not Found",
        408 => "Request Timeout",
        422 => "Unprocessable Entity",
        499 => "Client Closed Request",
        501 => "Not Implemented",
        502 => "Bad Gateway",
        500 => "Internal Server Error",
        _ => "Error"
    };
}
