using FluentValidation;
using MrBekoXBlogAppServer.Application.Features.CategoryFeature.Queries.GetByIdCategoryQuery;
using MrBekoXBlogAppServer.Application.Features.Constants;

namespace MrBekoXBlogAppServer.Application.Features.CategoryFeature.Validatiors;

public class GetByIdCategoryQueryRequestValidator : AbstractValidator<GetByIdCategoryQueryRequest>
{
    public GetByIdCategoryQueryRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(CategoryValidationMessages.CategoryIdRequired)
            .Must(id => Guid.TryParse(id, out _))
            .WithMessage(CategoryValidationMessages.CategoryIdMustBeValidGuid);

    }
}
