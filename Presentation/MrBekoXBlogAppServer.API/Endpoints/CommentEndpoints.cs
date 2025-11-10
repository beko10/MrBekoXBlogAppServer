using MediatR;
using MrBekoXBlogAppServer.Application.Features.CommentFeature.Commands.CreateCommentCommand;
using MrBekoXBlogAppServer.Application.Features.CommentFeature.Commands.DeleteCommentCommand;
using MrBekoXBlogAppServer.Application.Features.CommentFeature.Commands.UpdateCommentCommand;
using MrBekoXBlogAppServer.Application.Features.CommentFeature.DTOs;
using MrBekoXBlogAppServer.Application.Features.CommentFeature.Queries.GetAllCommentQuery;
using MrBekoXBlogAppServer.Application.Features.CommentFeature.Queries.GetByIdCommentQuery;

namespace MrBekoXBlogAppServer.API.Endpoints;

public static class CommentEndpoints
{
    public static IEndpointRouteBuilder RegisterCommentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/comments").WithTags("Comments");

        group.MapGet("/", async (IMediator mediator, CancellationToken cancellationToken) =>
        {
            var queryResult = await mediator.Send(new GetAllCommentQueryRequest(), cancellationToken);
            return queryResult.Result.IsSuccess
                ? Results.Ok(queryResult.Result.Data)
                : Results.BadRequest(queryResult.Result);
        })
        .WithName("GetAllComments")
        .Produces<GetAllCommentQueryResponse>(200)
        .Produces(400);

        group.MapGet("/{id}", async (string id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var queryResult = await mediator.Send(new GetByIdCommentQueryRequest { Id = id }, cancellationToken);
            return queryResult.Result.IsSuccess
                ? Results.Ok(queryResult.Result.Data)
                : Results.NotFound(queryResult.Result);
        })
        .WithName("GetByIdComment")
        .Produces<GetByIdCommentQueryResponse>(200)
        .Produces(404);

        group.MapPost("/", async (CreateCommentCommandDto request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var commandResult = await mediator.Send(new CreateCommentCommandRequest { CreateCommentCommandDtoRequest = request }, cancellationToken);
            return commandResult.Result.IsSuccess
                ? Results.Created($"/api/comments/{request.PostId}", commandResult.Result)
                : Results.BadRequest(commandResult.Result);
        })
        .WithName("CreateComment")
        .Produces<CreateCommentCommandResponse>(201)
        .Produces(400);

        group.MapPut("/{id}", async (string id, UpdateCommentCommandDto request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            request.Id = id;
            var commandResult = await mediator.Send(new UpdateCommentCommandRequest { UpdateCommentCommandDtoRequest = request }, cancellationToken);
            return commandResult.Result.IsSuccess
                ? Results.Ok(commandResult.Result)
                : Results.BadRequest(commandResult.Result);
        })
        .WithName("UpdateComment")
        .Produces<UpdateCommentCommandResponse>(200)
        .Produces(400);

        group.MapDelete("/{id}", async (string id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var commandResult = await mediator.Send(new DeleteCommentCommandRequest { Id = id }, cancellationToken);
            return commandResult.Result.IsSuccess
                ? Results.NoContent()
                : Results.BadRequest(commandResult.Result);
        })
        .WithName("DeleteComment")
        .Produces(204)
        .Produces(400);

        return app;
    }
}

