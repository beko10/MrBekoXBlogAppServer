using FluentValidation;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.Commands.LogoutUserCommand;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.Constants;

namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.Validatiors;

public class LogoutUserCommandValidator : AbstractValidator<LogoutUserCommandRequest>
{
    public LogoutUserCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithMessage(AuthValidationMessages.Logout.RefreshToken.NotEmpty)
            .MinimumLength(32)
            .WithMessage(AuthValidationMessages.Logout.RefreshToken.MinLength)
            .MaximumLength(500)
            .WithMessage(AuthValidationMessages.Logout.RefreshToken.MaxLength);

        RuleFor(x => x.Jti)
            .Matches(@"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$")
            .WithMessage(AuthValidationMessages.Logout.Jti.InvalidFormat)
            .When(x => !string.IsNullOrWhiteSpace(x.Jti));
    }
}

