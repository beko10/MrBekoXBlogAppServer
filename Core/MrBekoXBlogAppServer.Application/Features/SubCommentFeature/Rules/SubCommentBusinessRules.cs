using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Constants;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.CommentRepository;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.SubCommentRepository;
using System.Net;

namespace MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Rules;

public class SubCommentBusinessRules(
    ISubCommentReadRepository _subCommentReadRepository,
    ICommentReadRepository _commentReadRepository) : ISubCommentBusinessRules
{
    public Result SubCommentContentCannotBeEmpty(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            var exception = new BusinessRuleException(
                message: SubCommentBusinessRuleErrorMessages.ContentEmpty
            );
            return Result.Failure(
                error: exception,
                message: SubCommentBusinessRuleMessages.ContentEmpty,
                statusCode: (int)HttpStatusCode.BadRequest
            );
        }

        return Result.Success();
    }

    public Result SubCommentContentLengthMustBeValid(string content)
    {
        if (content.Length < 3)
        {
            var exception = new BusinessRuleException(
                message: SubCommentBusinessRuleErrorMessages.ContentTooShort
            );
            return Result.Failure(
                error: exception,
                message: SubCommentBusinessRuleMessages.ContentTooShort,
                statusCode: (int)HttpStatusCode.BadRequest
            );
        }

        if (content.Length > 500)
        {
            var exception = new BusinessRuleException(
                message: SubCommentBusinessRuleErrorMessages.ContentTooLong
            );
            return Result.Failure(
                error: exception,
                message: SubCommentBusinessRuleMessages.ContentTooLong,
                statusCode: (int)HttpStatusCode.BadRequest
            );
        }

        return Result.Success();
    }

    public async Task<Result> SubCommentMustExistAsync(string subCommentId)
    {
        var subComment = await _subCommentReadRepository.GetByIdAsync(subCommentId);

        if (subComment is null)
        {
            var exception = new BusinessRuleException(
                message: SubCommentBusinessRuleErrorMessages.NotFound
            );
            return Result.Failure(
                error: exception,
                message: SubCommentBusinessRuleMessages.NotFound,
                statusCode: (int)HttpStatusCode.NotFound
            );
        }
        return Result.Success();
    }

    public async Task<Result> CommentMustExistAsync(string commentId)
    {
        var comment = await _commentReadRepository.GetByIdAsync(commentId);

        if (comment is null)
        {
            var exception = new BusinessRuleException(
                message: SubCommentBusinessRuleErrorMessages.CommentNotFound
            );
            return Result.Failure(
                error: exception,
                message: SubCommentBusinessRuleMessages.CommentNotFound,
                statusCode: (int)HttpStatusCode.NotFound
            );
        }
        return Result.Success();
    }
}

