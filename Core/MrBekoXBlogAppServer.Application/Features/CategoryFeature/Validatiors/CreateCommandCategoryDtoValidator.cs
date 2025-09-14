using FluentValidation;
using MrBekoXBlogAppServer.Application.Features.CategoryFeature.DTOs;
using MrBekoXBlogAppServer.Application.Features.Constants;

namespace MrBekoXBlogAppServer.Application.Features.CategoryFeature.Validatiors;

public class CreateCommandCategoryDtoValidator : AbstractValidator<CreateCommandCategoryDto>
{
    public CreateCommandCategoryDtoValidator()
    {
        RuleFor(x => x.CategoryName)
            .NotEmpty().WithMessage(CategoryValidationMessages.CategoryIdRequired)
            .MaximumLength(100).WithMessage(CategoryValidationMessages.CategoryNameMaxLength);
    }
}
