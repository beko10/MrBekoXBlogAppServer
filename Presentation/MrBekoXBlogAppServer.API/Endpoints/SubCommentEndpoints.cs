using MediatR;
using MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Commands.CreateSubCommentCommand;
using MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Commands.DeleteSubCommentCommand;
using MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Commands.UpdateSubCommentCommand;
using MrBekoXBlogAppServer.Application.Features.SubCommentFeature.DTOs;
using MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Queries.GetAllSubCommentQuery;
using MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Queries.GetByIdSubCommentQuery;

namespace MrBekoXBlogAppServer.API.Endpoints;

public static class SubCommentEndpoints
{
    public static IEndpointRouteBuilder RegisterSubCommentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/subcomments").WithTags("SubComments");

        group.MapGet("/", async (IMediator mediator, CancellationToken cancellationToken) =>
        {
            var queryResult = await mediator.Send(new GetAllSubCommentQueryRequest(), cancellationToken);
            return queryResult.Result.IsSuccess
                ? Results.Ok(queryResult.Result.Data)
                : Results.BadRequest(queryResult.Result);
        })
        .WithName("GetAllSubComments")
        .Produces<GetAllSubCommentQueryResponse>(200)
        .Produces(400);

        group.MapGet("/{id}", async (string id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var queryResult = await mediator.Send(new GetByIdSubCommentQueryRequest { Id = id }, cancellationToken);
            return queryResult.Result.IsSuccess
                ? Results.Ok(queryResult.Result.Data)
                : Results.NotFound(queryResult.Result);
        })
        .WithName("GetByIdSubComment")
        .Produces<GetByIdSubCommentQueryResponse>(200)
        .Produces(404);

        group.MapPost("/", async (CreateSubCommentCommandDto request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var commandResult = await mediator.Send(new CreateSubCommentCommandRequest { CreateSubCommentCommandDtoRequest = request }, cancellationToken);
            return commandResult.Result.IsSuccess
                ? Results.Created($"/api/subcomments/{request.CommentId}", commandResult.Result)
                : Results.BadRequest(commandResult.Result);
        })
        .WithName("CreateSubComment")
        .Produces<CreateSubCommentCommandResponse>(201)
        .Produces(400);

        group.MapPut("/{id}", async (string id, UpdateSubCommentCommandDto request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            request.Id = id;
            var commandResult = await mediator.Send(new UpdateSubCommentCommandRequest { UpdateSubCommentCommandDtoRequest = request }, cancellationToken);
            return commandResult.Result.IsSuccess
                ? Results.Ok(commandResult.Result)
                : Results.BadRequest(commandResult.Result);
        })
        .WithName("UpdateSubComment")
        .Produces<UpdateSubCommentCommandResponse>(200)
        .Produces(400);

        group.MapDelete("/{id}", async (string id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var commandResult = await mediator.Send(new DeleteSubCommentCommandRequest { Id = id }, cancellationToken);
            return commandResult.Result.IsSuccess
                ? Results.NoContent()
                : Results.BadRequest(commandResult.Result);
        })
        .WithName("DeleteSubComment")
        .Produces(204)
        .Produces(400);

        return app;
    }
}

