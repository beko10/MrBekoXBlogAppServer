using FluentValidation;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.DTOs;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.Constants;

namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.Validatiors;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommandDto>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(AuthValidationMessages.Register.Email.NotEmpty)
            .EmailAddress()
            .WithMessage(AuthValidationMessages.Register.Email.InvalidFormat)
            .MaximumLength(256)
            .WithMessage(AuthValidationMessages.Register.Email.MaxLength);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(AuthValidationMessages.Register.Password.NotEmpty)
            .MinimumLength(8)
            .WithMessage(AuthValidationMessages.Register.Password.MinLength)
            .Matches(@"[A-Z]")
            .WithMessage(AuthValidationMessages.Register.Password.MustContainUppercase)
            .Matches(@"[a-z]")
            .WithMessage(AuthValidationMessages.Register.Password.MustContainLowercase)
            .Matches(@"\d")
            .WithMessage(AuthValidationMessages.Register.Password.MustContainDigit)
            .Matches(@"[^a-zA-Z0-9]")
            .WithMessage(AuthValidationMessages.Register.Password.MustContainSpecialChar);

        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage(AuthValidationMessages.Register.FullName.NotEmpty)
            .MinimumLength(2)
            .WithMessage(AuthValidationMessages.Register.FullName.MinLength)
            .MaximumLength(100)
            .WithMessage(AuthValidationMessages.Register.FullName.MaxLength);

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
            .WithMessage(AuthValidationMessages.Register.PhoneNumber.InvalidFormat);
    }
}
