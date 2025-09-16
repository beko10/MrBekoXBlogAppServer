using FluentValidation;
using MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.Constants;
using MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.Queries.GetByIdContactInfoQuery;

namespace MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.Validators;

public class GetByIdContactInfoQueryRequestValidator : AbstractValidator<GetByIdContactInfoQueryRequest>
{
    public GetByIdContactInfoQueryRequestValidator()
    {
        RuleFor(x => x.Id)
           .NotEmpty()
           .WithMessage(ContactInfoValidationMessages.GetById.IdRequired);

        RuleFor(x => x.Id)
            .Must(BeValidGuid)
            .WithMessage(ContactInfoValidationMessages.GetById.IdMustBeValidGuid)
            .When(x => !string.IsNullOrEmpty(x.Id));
    }

    private bool BeValidGuid(string id)
    {
        return Guid.TryParse(id, out _);
    }
}
