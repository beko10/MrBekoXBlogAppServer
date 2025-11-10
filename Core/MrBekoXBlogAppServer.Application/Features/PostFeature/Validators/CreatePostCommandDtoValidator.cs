using FluentValidation;
using MrBekoXBlogAppServer.Application.Features.PostFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.PostFeature.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.PostFeature.Validators;

public class CreatePostCommandDtoValidator : AbstractValidator<CreatePostCommandDto>
{
    public CreatePostCommandDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage(PostValidationMessages.TitleRequired)
            .MinimumLength(3).WithMessage(PostValidationMessages.TitleMinLength)
            .MaximumLength(200).WithMessage(PostValidationMessages.TitleMaxLength);

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage(PostValidationMessages.ContentRequired)
            .MinimumLength(10).WithMessage(PostValidationMessages.ContentMinLength);

        RuleFor(x => x.Author)
            .NotEmpty().WithMessage(PostValidationMessages.AuthorRequired)
            .MinimumLength(2).WithMessage(PostValidationMessages.AuthorMinLength)
            .MaximumLength(100).WithMessage(PostValidationMessages.AuthorMaxLength);

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage(PostValidationMessages.CategoryIdRequired)
            .Must(id => Guid.TryParse(id, out _))
            .WithMessage(PostValidationMessages.CategoryIdMustBeValidGuid);

        RuleFor(x => x.CoverImageUrl)
            .MaximumLength(500).WithMessage(PostValidationMessages.CoverImageUrlMaxLength)
            .When(x => !string.IsNullOrEmpty(x.CoverImageUrl));

        RuleFor(x => x.PostImageUrl)
            .MaximumLength(500).WithMessage(PostValidationMessages.PostImageUrlMaxLength)
            .When(x => !string.IsNullOrEmpty(x.PostImageUrl));
    }
}

