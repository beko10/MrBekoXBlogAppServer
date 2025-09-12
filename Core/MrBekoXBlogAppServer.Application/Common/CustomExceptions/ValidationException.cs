using MrBekoXBlogAppServer.Application.Common.CustomExceptions;

public class ValidationException : AppException
{
    public IReadOnlyList<string> Errors { get; }

    public ValidationException(string message, IEnumerable<string> errors)
        : base(message, 400, "VALIDATION_FAILED")
    {
        Errors = errors.ToList();
    }

    public ValidationException(IEnumerable<string> errors)
        : this("Validation failed", errors) { }
}