using FluentValidation;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.Commands.LoginUserCommand;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.Constants;

namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.Validatiors;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommandRequest>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(AuthValidationMessages.Login.Email.NotEmpty)
            .EmailAddress()
            .WithMessage(AuthValidationMessages.Login.Email.InvalidFormat)
            .MaximumLength(256)
            .WithMessage(AuthValidationMessages.Login.Email.MaxLength);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(AuthValidationMessages.Login.Password.NotEmpty)
            .MinimumLength(8)
            .WithMessage(AuthValidationMessages.Login.Password.MinLength)
            .MaximumLength(128)
            .WithMessage(AuthValidationMessages.Login.Password.MaxLength);
    }
}
