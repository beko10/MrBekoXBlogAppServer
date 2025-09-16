using FluentValidation;
using MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.Constants;
using MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.Validators;

public class CreateContactInfoDtoValidator : AbstractValidator<CreateContactInfoDto>
{
    public CreateContactInfoDtoValidator()
    {
        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage(ContactInfoValidationMessages.Create.Address.NotEmpty)
            .MinimumLength(10)
            .WithMessage(ContactInfoValidationMessages.Create.Address.MinLength)
            .MaximumLength(500)
            .WithMessage(ContactInfoValidationMessages.Create.Address.MaxLength);

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(ContactInfoValidationMessages.Create.Email.NotEmpty)
            .EmailAddress()
            .WithMessage(ContactInfoValidationMessages.Create.Email.InvalidFormat)
            .MaximumLength(100)
            .WithMessage(ContactInfoValidationMessages.Create.Email.MaxLength);

        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage(ContactInfoValidationMessages.Create.Phone.NotEmpty)
            .MinimumLength(10)
            .WithMessage(ContactInfoValidationMessages.Create.Phone.MinLength)
            .MaximumLength(15)
            .WithMessage(ContactInfoValidationMessages.Create.Phone.MaxLength)
            .Matches(ContactInfoRegexPatterns.Phone)
            .WithMessage(ContactInfoValidationMessages.Create.Phone.InvalidFormat);

        RuleFor(x => x.MapUrl)
            .NotEmpty()
            .WithMessage(ContactInfoValidationMessages.Create.MapUrl.NotEmpty)
            .Must(BeAValidUrl)
            .WithMessage(ContactInfoValidationMessages.Create.MapUrl.InvalidFormat)
            .MaximumLength(1000)
            .WithMessage(ContactInfoValidationMessages.Create.MapUrl.MaxLength);
    }

    private bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}
