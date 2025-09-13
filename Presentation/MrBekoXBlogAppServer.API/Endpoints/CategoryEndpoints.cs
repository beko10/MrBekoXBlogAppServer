using MediatR;
using MrBekoXBlogAppServer.Application.Features.CategoryFeature.Queries.GetByIdCategoryQuery;

namespace MrBekoXBlogAppServer.API.Endpoints;

public static class CategoryEndpoints 
{
    public static IEndpointRouteBuilder RegisterCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(prefix:"/categories").WithTags("Categories");

        group.MapGet("/{id}", async(string id,IMediator _mediator) =>
        {
            var queryResult = await _mediator.Send(new GetByIdCategoryQueryRequest { Id = id});
            if (queryResult.Result.IsSuccess)
            {
                return Results.Ok(queryResult.Result);
            }
            return Results.BadRequest(queryResult.Result);
        })
            .WithName("GetByIdCategory")
            .Produces<GetByIdCategoryQueryResponse>(200)
            .Produces(400);


        return app; 
    }
}
