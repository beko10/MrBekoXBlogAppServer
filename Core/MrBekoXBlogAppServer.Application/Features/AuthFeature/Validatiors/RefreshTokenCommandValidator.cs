using FluentValidation;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.Commands.RefreshTokenCommand;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.Constants;

namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.Validatiors;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommandRequest>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithMessage(AuthValidationMessages.RefreshToken.Token.NotEmpty)
            .MinimumLength(32)
            .WithMessage(AuthValidationMessages.RefreshToken.Token.MinLength)
            .MaximumLength(500)
            .WithMessage(AuthValidationMessages.RefreshToken.Token.MaxLength);
    }
}

