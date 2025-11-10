using FluentValidation;
using MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.SubCommentFeature.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Validators;

public class UpdateSubCommentCommandDtoValidator : AbstractValidator<UpdateSubCommentCommandDto>
{
    public UpdateSubCommentCommandDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(SubCommentValidationMessages.SubCommentIdRequired)
            .Must(id => Guid.TryParse(id, out _))
            .WithMessage(SubCommentValidationMessages.SubCommentIdMustBeValidGuid);

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage(SubCommentValidationMessages.ContentRequired)
            .MinimumLength(3).WithMessage(SubCommentValidationMessages.ContentMinLength)
            .MaximumLength(500).WithMessage(SubCommentValidationMessages.ContentMaxLength);
    }
}

