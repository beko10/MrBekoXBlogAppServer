using FluentValidation;
using MrBekoXBlogAppServer.Application.Features.CommentFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.CommentFeature.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.CommentFeature.Validators;

public class UpdateCommentCommandDtoValidator : AbstractValidator<UpdateCommentCommandDto>
{
    public UpdateCommentCommandDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(CommentValidationMessages.CommentIdRequired)
            .Must(id => Guid.TryParse(id, out _))
            .WithMessage(CommentValidationMessages.CommentIdMustBeValidGuid);

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage(CommentValidationMessages.ContentRequired)
            .MinimumLength(3).WithMessage(CommentValidationMessages.ContentMinLength)
            .MaximumLength(500).WithMessage(CommentValidationMessages.ContentMaxLength);
    }
}

