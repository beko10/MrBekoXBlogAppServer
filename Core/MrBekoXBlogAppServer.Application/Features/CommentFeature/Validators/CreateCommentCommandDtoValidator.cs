using FluentValidation;
using MrBekoXBlogAppServer.Application.Features.CommentFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.CommentFeature.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.CommentFeature.Validators;

public class CreateCommentCommandDtoValidator : AbstractValidator<CreateCommentCommandDto>
{
    public CreateCommentCommandDtoValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage(CommentValidationMessages.ContentRequired)
            .MinimumLength(3).WithMessage(CommentValidationMessages.ContentMinLength)
            .MaximumLength(500).WithMessage(CommentValidationMessages.ContentMaxLength);

        RuleFor(x => x.PostId)
            .NotEmpty().WithMessage(CommentValidationMessages.PostIdRequired)
            .Must(id => Guid.TryParse(id, out _))
            .WithMessage(CommentValidationMessages.PostIdMustBeValidGuid);
    }
}

