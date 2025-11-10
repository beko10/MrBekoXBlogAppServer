using MrBekoXBlogAppServer.API.Endpoints;

namespace MrBekoXBlogAppServer.API.Extensions;

public static class RegisterEndpointsExtensions
{
    public static IEndpointRouteBuilder RegisterAllEndpoints(this IEndpointRouteBuilder app)
    {
        app.RegisterCategoryEndpoints();
        app.RegisterAuthEndpoints();
        app.RegisterCommentEndpoints();
        app.RegisterSubCommentEndpoints();
        app.RegisterPostEndpoints();
        return app;
    }
}
