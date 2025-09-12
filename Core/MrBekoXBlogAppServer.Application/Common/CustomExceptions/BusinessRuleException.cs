using MrBekoXBlogAppServer.Application.Common.CustomExceptions;

public class BusinessRuleException : AppException
{
    public BusinessRuleException(string message)
        : base(message, 422, "BUSINESS_RULE_VIOLATION") { }
}