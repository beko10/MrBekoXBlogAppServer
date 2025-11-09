using MediatR;
using Microsoft.AspNetCore.Identity;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Domain.Entities;

namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.Commands.RegisterUserCommand;

public class RegisterUserCommandHandler(
    UserManager<AppUser> userManager 
    ) : IRequestHandler<RegisterUserCommandRequest, RegisterUserCommandResponse>
{
    public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
    {
        var user = new AppUser
        {
            UserName = request.registerUserCommandDtorequest!.UserName,
            Email = request.registerUserCommandDtorequest.Email,
            FullName = request.registerUserCommandDtorequest.FullName,
            PhoneNumber = request.registerUserCommandDtorequest.PhoneNumber,
            ImageUrl = "https://www.gravatar.com/avatar/" + Guid.NewGuid().ToString() + "?d=mp",
        };

        var result = await userManager.CreateAsync(user, request.registerUserCommandDtorequest.Password);

        if(!result.Succeeded)
        {
            var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
            return new RegisterUserCommandResponse
            {
                Result = Result.Failure($"User creation failed : {errorMessages}")
            };
        }

        await userManager.AddToRoleAsync(user, "User");

        return new RegisterUserCommandResponse
        {
            Result = Result.Success("User created successfully.", 201)
        };
    }
}
