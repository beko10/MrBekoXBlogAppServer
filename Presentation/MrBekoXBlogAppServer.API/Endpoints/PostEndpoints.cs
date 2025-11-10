using MediatR;
using MrBekoXBlogAppServer.Application.Features.PostFeature.Commands.CreatePostCommand;
using MrBekoXBlogAppServer.Application.Features.PostFeature.Commands.DeletePostCommand;
using MrBekoXBlogAppServer.Application.Features.PostFeature.Commands.UpdatePostCommand;
using MrBekoXBlogAppServer.Application.Features.PostFeature.DTOs;
using MrBekoXBlogAppServer.Application.Features.PostFeature.Queries.GetAllPostQuery;
using MrBekoXBlogAppServer.Application.Features.PostFeature.Queries.GetByIdPostQuery;

namespace MrBekoXBlogAppServer.API.Endpoints;

public static class PostEndpoints
{
    public static IEndpointRouteBuilder RegisterPostEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/posts").WithTags("Posts");

        group.MapGet("/", async (IMediator mediator, CancellationToken cancellationToken) =>
        {
            var queryResult = await mediator.Send(new GetAllPostQueryRequest(), cancellationToken);
            return queryResult.Result.IsSuccess
                ? Results.Ok(queryResult.Result.Data)
                : Results.BadRequest(queryResult.Result);
        })
        .WithName("GetAllPosts")
        .Produces<GetAllPostQueryResponse>(200)
        .Produces(400);

        group.MapGet("/{id}", async (string id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var queryResult = await mediator.Send(new GetByIdPostQueryRequest { Id = id }, cancellationToken);
            return queryResult.Result.IsSuccess
                ? Results.Ok(queryResult.Result.Data)
                : Results.NotFound(queryResult.Result);
        })
        .WithName("GetByIdPost")
        .Produces<GetByIdPostQueryResponse>(200)
        .Produces(404);

        group.MapPost("/", async (CreatePostCommandDto request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var commandResult = await mediator.Send(new CreatePostCommandRequest { CreatePostCommandDtoRequest = request }, cancellationToken);
            return commandResult.Result.IsSuccess
                ? Results.Created($"/api/posts/{request.CategoryId}", commandResult.Result)
                : Results.BadRequest(commandResult.Result);
        })
        .WithName("CreatePost")
        .Produces<CreatePostCommandResponse>(201)
        .Produces(400);

        group.MapPut("/{id}", async (string id, UpdatePostCommandDto request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            request.Id = id;
            var commandResult = await mediator.Send(new UpdatePostCommandRequest { UpdatePostCommandDtoRequest = request }, cancellationToken);
            return commandResult.Result.IsSuccess
                ? Results.Ok(commandResult.Result)
                : Results.BadRequest(commandResult.Result);
        })
        .WithName("UpdatePost")
        .Produces<UpdatePostCommandResponse>(200)
        .Produces(400);

        group.MapDelete("/{id}", async (string id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var commandResult = await mediator.Send(new DeletePostCommandRequest { Id = id }, cancellationToken);
            return commandResult.Result.IsSuccess
                ? Results.NoContent()
                : Results.BadRequest(commandResult.Result);
        })
        .WithName("DeletePost")
        .Produces(204)
        .Produces(400);

        return app;
    }
}

