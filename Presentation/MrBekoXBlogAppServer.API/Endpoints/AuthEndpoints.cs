using MediatR;
using Microsoft.AspNetCore.Authorization;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.Commands.ConfirmEmailCommand;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.Commands.LoginUserCommand;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.Commands.LogoutUserCommand;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.Commands.RefreshTokenCommand;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.Commands.RegisterUserCommand;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.DTOs;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.Queries.GetActiveSessionsQuery;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.Queries.GetUserProfileQuery;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MrBekoXBlogAppServer.API.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder RegisterAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/auth").WithTags("Authentication");

        // Public endpoints (no authentication required)
        group.MapPost("/register", async (RegisterUserCommandDto request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var commandResult = await mediator.Send(new RegisterUserCommandRequest
            {
                registerUserCommandDtorequest = request
            }, cancellationToken);

            return commandResult.Result.IsSuccess
                ? Results.Created($"/api/auth/profile", commandResult.Result)
                : Results.BadRequest(commandResult.Result);
        })
        .WithName("RegisterUser")
        .Produces<RegisterUserCommandResponse>(201)
        .Produces(400)
        .AllowAnonymous();

        group.MapGet("/confirm-email", async (string userId, string token, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var commandResult = await mediator.Send(new ConfirmEmailCommandRequest
            {
                UserId = userId,
                Token = token
            }, cancellationToken);

            return commandResult.Result.IsSuccess
                ? Results.Ok(commandResult.Result)
                : Results.BadRequest(commandResult.Result);
        })
        .WithName("ConfirmEmail")
        .Produces<ConfirmEmailCommandResponse>(200)
        .Produces(400)
        .Produces(404)
        .AllowAnonymous();

        group.MapPost("/login", async (LoginUserCommandRequest request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var commandResult = await mediator.Send(request, cancellationToken);

            if (!commandResult.Result.IsSuccess)
            {
                return commandResult.Result.StatusCode switch
                {
                    401 => Results.Unauthorized(),
                    400 => Results.BadRequest(commandResult.Result),
                    _ => Results.BadRequest(commandResult.Result)
                };
            }

            return Results.Ok(commandResult.Result);
        })
        .WithName("LoginUser")
        .Produces<LoginUserCommandResponse>(200)
        .Produces(401)
        .Produces(400)
        .AllowAnonymous();

        group.MapPost("/refresh", async (RefreshTokenCommandRequest request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var commandResult = await mediator.Send(request, cancellationToken);

            if (!commandResult.Result.IsSuccess)
            {
                return commandResult.Result.StatusCode switch
                {
                    401 => Results.Unauthorized(),
                    400 => Results.BadRequest(commandResult.Result),
                    _ => Results.BadRequest(commandResult.Result)
                };
            }

            return Results.Ok(commandResult.Result);
        })
        .WithName("RefreshToken")
        .Produces<RefreshTokenCommandResponse>(200)
        .Produces(401)
        .Produces(400)
        .AllowAnonymous();

        // Protected endpoints (authentication required)
        group.MapPost("/logout", [Authorize] async (LogoutUserCommandRequest request, IMediator mediator, HttpContext httpContext, CancellationToken cancellationToken) =>
        {
            // JTI'yi JWT token'dan al (eğer request'te yoksa)
            if (string.IsNullOrEmpty(request.Jti))
            {
                request.Jti = httpContext.User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
            }

            var commandResult = await mediator.Send(request, cancellationToken);

            return commandResult.Result.IsSuccess
                ? Results.Ok(commandResult.Result)
                : Results.BadRequest(commandResult.Result);
        })
        .WithName("LogoutUser")
        .Produces<LogoutUserCommandResponse>(200)
        .Produces(400)
        .RequireAuthorization();

        group.MapGet("/profile", [Authorize] async (IMediator mediator, CancellationToken cancellationToken) =>
        {
            var queryResult = await mediator.Send(new GetUserProfileQueryRequest(), cancellationToken);

            if (!queryResult.Result.IsSuccess)
            {
                return queryResult.Result.StatusCode switch
                {
                    401 => Results.Unauthorized(),
                    404 => Results.NotFound(queryResult.Result),
                    _ => Results.BadRequest(queryResult.Result)
                };
            }

            return Results.Ok(queryResult.Result);
        })
        .WithName("GetUserProfile")
        .Produces<GetUserProfileQueryResponse>(200)
        .Produces(401)
        .Produces(404)
        .RequireAuthorization();

        group.MapGet("/sessions", [Authorize] async (IMediator mediator, CancellationToken cancellationToken) =>
        {
            var queryResult = await mediator.Send(new GetActiveSessionsQueryRequest(), cancellationToken);

            if (!queryResult.Result.IsSuccess)
            {
                return queryResult.Result.StatusCode switch
                {
                    401 => Results.Unauthorized(),
                    _ => Results.BadRequest(queryResult.Result)
                };
            }

            return Results.Ok(queryResult.Result);
        })
        .WithName("GetActiveSessions")
        .Produces<GetActiveSessionsQueryResponse>(200)
        .Produces(401)
        .Produces(400)
        .RequireAuthorization();

        return app;
    }
}

