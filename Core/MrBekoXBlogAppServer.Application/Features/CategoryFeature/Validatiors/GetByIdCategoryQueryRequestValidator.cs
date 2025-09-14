using FluentValidation;
using MrBekoXBlogAppServer.Application.Features.CategoryFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.CategoryFeature.Queries.GetByIdCategoryQuery;

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
