namespace MrBekoXBlogAppServer.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
    {
        services.AddTransient<Middleware.ExceptionHandlingMiddleware>();
        return services;
    }
}
