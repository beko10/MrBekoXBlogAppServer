using FluentValidation;
using MrBekoXBlogAppServer.Application.Features.ContactInfoFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.ContactInfoFeature.Queries.GetByIdContactInfoQuery;

namespace MrBekoXBlogAppServer.Application.Features.ContactInfoFeature.Validators;

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
