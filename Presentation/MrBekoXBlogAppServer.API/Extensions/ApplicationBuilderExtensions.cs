namespace MrBekoXBlogAppServer.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<Middleware.ExceptionHandlingMiddleware>();
    }
}
