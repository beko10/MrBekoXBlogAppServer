using MrBekoXBlogAppServer.API.Endpoints;

namespace MrBekoXBlogAppServer.API.Extensions;

public static class RegisterEndpointsExtensions
{
    public static IEndpointRouteBuilder RegisterAllEndpoints(this IEndpointRouteBuilder app)
    {
        app.RegisterCategoryEndpoints();
        return app;
    }
}
