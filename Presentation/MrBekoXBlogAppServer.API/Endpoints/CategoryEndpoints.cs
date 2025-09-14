using MediatR;
using MrBekoXBlogAppServer.Application.Features.CategoryFeature.Commands.CreateCategoryCommand;
using MrBekoXBlogAppServer.Application.Features.CategoryFeature.Commands.DeleteCategoryCommand;
using MrBekoXBlogAppServer.Application.Features.CategoryFeature.Commands.UpdateCategoryCommand;
using MrBekoXBlogAppServer.Application.Features.CategoryFeature.DTOs;
using MrBekoXBlogAppServer.Application.Features.CategoryFeature.Queries.GetAllCategoryQuery;
using MrBekoXBlogAppServer.Application.Features.CategoryFeature.Queries.GetByIdCategoryQuery;

namespace MrBekoXBlogAppServer.API.Endpoints;

public static class CategoryEndpoints 
{
    public static IEndpointRouteBuilder RegisterCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/categories").WithTags("Categories");

        group.MapGet("/", async (IMediator mediator, CancellationToken cancellationToken) =>
        {
            var queryResult = await mediator.Send(new GetAllCategoryQueryRequest(), cancellationToken);
            return queryResult.Result.IsSuccess
                ? Results.Ok(queryResult.Result.Data)
                : Results.BadRequest(queryResult.Result);
        })
        .WithName("GetAllCategories")
        .Produces<GetAllCategoryQueryResponse>(200)
        .Produces(400);

        group.MapGet("/{id}", async (string id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var queryResult = await mediator.Send(new GetByIdCategoryQueryRequest { Id = id }, cancellationToken);
            return queryResult.Result.IsSuccess
                ? Results.Ok(queryResult.Result.Data)
                : Results.NotFound(queryResult.Result);
        })
        .WithName("GetByIdCategory")
        .Produces<GetByIdCategoryQueryResponse>(200)
        .Produces(404);

        
        group.MapPost("/", async (CreateCommandCategoryDto request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var commandResult = await mediator.Send(new CreateCategoryCommandRequest { CreateCommandCategoryDtoRequest = request }, cancellationToken);
            return commandResult.Result.IsSuccess
                ? Results.Ok(commandResult.Result)
                : Results.BadRequest(commandResult.Result);
        })
        .WithName("CreateCategory")
        .Produces<CreateCategoryCommandResponse>(201)
        .Produces(400);

      
        group.MapPut("/{id}", async (string id,UpdateCommandCategoryDto request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            
            request.Id = id;
            var commandResult = await mediator.Send(new UpdateCategoryCommandRequest { UpdateCommandCategoryDtoRequest = request }, cancellationToken);
            return commandResult.Result.IsSuccess
                ? Results.Ok(commandResult.Result)
                : Results.BadRequest(commandResult.Result);
        })
        .WithName("UpdateCategory")
        .Produces<UpdateCategoryCommandResponse>(200)
        .Produces(400);

        group.MapDelete("/{id}", async (string id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var commandResult = await mediator.Send(new DeleteCategoryCommandRequest { Id = id }, cancellationToken);
            return commandResult.Result.IsSuccess
                ? Results.NoContent()
                : Results.BadRequest(commandResult.Result);
        })
        .WithName("DeleteCategory")
        .Produces(204)
        .Produces(400);

        return app;
    }
}
