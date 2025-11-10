using MediatR;
using Microsoft.AspNetCore.Identity;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.Constants;
using MrBekoXBlogAppServer.Domain.Entities;

namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.Commands.ConfirmEmailCommand;

public class ConfirmEmailCommandHandler(
    UserManager<AppUser> userManager
    ) : IRequestHandler<ConfirmEmailCommandRequest, ConfirmEmailCommandResponse>
{
    public async Task<ConfirmEmailCommandResponse> Handle(ConfirmEmailCommandRequest request, CancellationToken cancellationToken)
    {
        // 1. Validate inputs
        if (string.IsNullOrWhiteSpace(request.UserId) || string.IsNullOrWhiteSpace(request.Token))
        {
            return new ConfirmEmailCommandResponse
            {
                Result = Result.Failure("Invalid confirmation link", 400)
            };
        }

        // 2. Find user
        var user = await userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            return new ConfirmEmailCommandResponse
            {
                Result = Result.Failure("User not found", 404)
            };
        }

        // 3. Check if already confirmed
        if (user.EmailConfirmed)
        {
            return new ConfirmEmailCommandResponse
            {
                Result = Result.Success("Email already confirmed", 200)
            };
        }

        // 4. Confirm email
        var result = await userManager.ConfirmEmailAsync(user, request.Token);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return new ConfirmEmailCommandResponse
            {
                Result = Result.Failure($"Email confirmation failed: {errors}", 400)
            };
        }

        return new ConfirmEmailCommandResponse
        {
            Result = Result.Success("Email confirmed successfully. You can now login.", 200)
        };
    }
}
