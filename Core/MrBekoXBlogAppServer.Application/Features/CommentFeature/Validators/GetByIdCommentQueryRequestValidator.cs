using FluentValidation;
using MrBekoXBlogAppServer.Application.Features.CommentFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.CommentFeature.Queries.GetByIdCommentQuery;

namespace MrBekoXBlogAppServer.Application.Features.CommentFeature.Validators;

public class GetByIdCommentQueryRequestValidator : AbstractValidator<GetByIdCommentQueryRequest>
{
    public GetByIdCommentQueryRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(CommentValidationMessages.CommentIdRequired)
            .Must(id => Guid.TryParse(id, out _))
            .WithMessage(CommentValidationMessages.CommentIdMustBeValidGuid);
    }
}

