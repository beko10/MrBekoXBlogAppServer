using FluentValidation;
using MrBekoXBlogAppServer.Application.Features.CategoryFeature.DTOs;
using MrBekoXBlogAppServer.Application.Features.Constants.GetByIdCategoryQueryConstants;

namespace MrBekoXBlogAppServer.Application.Features.CategoryFeature.Validatiors;

public class GetByIdCategoryQueryValidator : AbstractValidator<ResultCategoryQueryDto>
{
    public GetByIdCategoryQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(CategoryValidationMessages.IdRequired);
        RuleFor(x => x.CategoryName)
            .NotEmpty()
            .WithMessage(CategoryValidationMessages.NameRequired);
        

    }
}
