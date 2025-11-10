using FluentValidation;
using MrBekoXBlogAppServer.Application.Features.PostFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.PostFeature.Queries.GetByIdPostQuery;

namespace MrBekoXBlogAppServer.Application.Features.PostFeature.Validators;

public class GetByIdPostQueryRequestValidator : AbstractValidator<GetByIdPostQueryRequest>
{
    public GetByIdPostQueryRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(PostValidationMessages.PostIdRequired)
            .Must(id => Guid.TryParse(id, out _))
            .WithMessage(PostValidationMessages.PostIdMustBeValidGuid);
    }
}

