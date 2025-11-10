using FluentValidation;
using MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Queries.GetByIdSubCommentQuery;

namespace MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Validators;

public class GetByIdSubCommentQueryRequestValidator : AbstractValidator<GetByIdSubCommentQueryRequest>
{
    public GetByIdSubCommentQueryRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(SubCommentValidationMessages.SubCommentIdRequired)
            .Must(id => Guid.TryParse(id, out _))
            .WithMessage(SubCommentValidationMessages.SubCommentIdMustBeValidGuid);
    }
}

