using MediatR;
using Microsoft.AspNetCore.Identity;
using MrBekoXBlogAppServer.Application.Common.BusinessRuleEngines;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.Rules;
using MrBekoXBlogAppServer.Application.Interfaces.Email;
using MrBekoXBlogAppServer.Domain.Entities;

namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.Commands.RegisterUserCommand;

public class RegisterUserCommandHandler(
    UserManager<AppUser> userManager,
    RoleManager<AppRole> roleManager,
    IEmailService emailService,
    IAuthBusinessRules authBusinessRules
    ) : IRequestHandler<RegisterUserCommandRequest, RegisterUserCommandResponse>
{
    public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
    {
        // 1. Business Rules: Run all register validation rules using BusinessRuleEngine
        var ruleResult = await BusinessRuleEngine.RunAsync(
            () => Task.FromResult(authBusinessRules.EmailFormatMustBeValid(request.registerUserCommandDtorequest!.Email)),
            () => authBusinessRules.EmailMustBeUniqueAsync(request.registerUserCommandDtorequest.Email, cancellationToken),
            () => authBusinessRules.UsernameMustBeUniqueAsync(request.registerUserCommandDtorequest.UserName, cancellationToken),
            () => Task.FromResult(authBusinessRules.PasswordMustMeetRequirements(request.registerUserCommandDtorequest.Password)),
            () => authBusinessRules.UserCountMustBeUnderLimitAsync(10000, cancellationToken)
        );

        if (ruleResult.IsFailure)
        {
            return new RegisterUserCommandResponse
            {
                Result = ruleResult
            };
        }

        // 2. Kullanıcı oluştur
        var user = new AppUser
        {
            UserName = request.registerUserCommandDtorequest.UserName,
            Email = request.registerUserCommandDtorequest.Email,
            FullName = request.registerUserCommandDtorequest.FullName,
            PhoneNumber = request.registerUserCommandDtorequest.PhoneNumber,
            //EmailConfirmed = false,
            ImageUrl = "https://www.gravatar.com/avatar/" + Guid.NewGuid().ToString() + "?d=mp",
        };

        var result = await userManager.CreateAsync(user, request.registerUserCommandDtorequest.Password);

        if (!result.Succeeded)
        {
            var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
            return new RegisterUserCommandResponse
            {
                Result = Result.Failure($"{AuthOperationResultMessages.UserCreationFailed}: {errorMessages}", 400)
            };
        }

        // 3. "User" rolü yoksa oluştur
        if (!await roleManager.RoleExistsAsync("User"))
        {
            await roleManager.CreateAsync(new AppRole { Name = "User" });
        }

        // 4. Kullanıcıya "User" rolünü ekle
        await userManager.AddToRoleAsync(user, "User");

        // 5. Email confirmation token oluştur ve aktivasyon maili gönder
        var emailConfirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(user);

        try
        {
            await emailService.SendActivationEmailAsync(
                user.Email!,
                user.FullName,
                emailConfirmationToken
            );
        }
        catch (Exception)
        {
            // Email gönderimi başarısız olsa bile kullanıcı kaydı başarılı
            // Log eklenebilir
        }

        return new RegisterUserCommandResponse
        {
            Result = Result.Success(AuthOperationResultMessages.RegisterSuccess, 201)
        };
    }
}