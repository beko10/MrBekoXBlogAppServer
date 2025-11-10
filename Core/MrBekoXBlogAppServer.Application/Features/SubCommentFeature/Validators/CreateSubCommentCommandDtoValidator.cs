using FluentValidation;
using MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.SubCommentFeature.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Validators;

public class CreateSubCommentCommandDtoValidator : AbstractValidator<CreateSubCommentCommandDto>
{
    public CreateSubCommentCommandDtoValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage(SubCommentValidationMessages.ContentRequired)
            .MinimumLength(3).WithMessage(SubCommentValidationMessages.ContentMinLength)
            .MaximumLength(500).WithMessage(SubCommentValidationMessages.ContentMaxLength);

        RuleFor(x => x.CommentId)
            .NotEmpty().WithMessage(SubCommentValidationMessages.CommentIdRequired)
            .Must(id => Guid.TryParse(id, out _))
            .WithMessage(SubCommentValidationMessages.CommentIdMustBeValidGuid);
    }
}

