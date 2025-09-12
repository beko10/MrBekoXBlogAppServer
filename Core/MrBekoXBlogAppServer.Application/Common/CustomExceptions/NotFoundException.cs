using MrBekoXBlogAppServer.Application.Common.CustomExceptions;

public class NotFoundException : AppException
{
    public NotFoundException(string entityName, object key)
        : base($"{entityName} with identifier '{key}' was not found", 404, "ENTITY_NOT_FOUND") { }

    public NotFoundException(string message)
        : base(message, 404, "NOT_FOUND") { }
}