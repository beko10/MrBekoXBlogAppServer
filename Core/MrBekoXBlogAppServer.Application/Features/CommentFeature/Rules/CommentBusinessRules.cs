using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.CommentFeature.Constants;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.CommentRepository;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.PostRepository;
using System.Net;

namespace MrBekoXBlogAppServer.Application.Features.CommentFeature.Rules;

public class CommentBusinessRules(
    ICommentReadRepository _commentReadRepository,
    IPostReadRepository _postReadRepository) : ICommentBusinessRules
{
    public Result CommentContentCannotBeEmpty(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            var exception = new BusinessRuleException(
                message: CommentBusinessRuleErrorMessages.ContentEmpty
            );
            return Result.Failure(
                error: exception,
                message: CommentBusinessRuleMessages.ContentEmpty,
                statusCode: (int)HttpStatusCode.BadRequest
            );
        }

        return Result.Success();
    }

    public Result CommentContentLengthMustBeValid(string content)
    {
        if (content.Length < 3)
        {
            var exception = new BusinessRuleException(
                message: CommentBusinessRuleErrorMessages.ContentTooShort
            );
            return Result.Failure(
                error: exception,
                message: CommentBusinessRuleMessages.ContentTooShort,
                statusCode: (int)HttpStatusCode.BadRequest
            );
        }

        if (content.Length > 500)
        {
            var exception = new BusinessRuleException(
                message: CommentBusinessRuleErrorMessages.ContentTooLong
            );
            return Result.Failure(
                error: exception,
                message: CommentBusinessRuleMessages.ContentTooLong,
                statusCode: (int)HttpStatusCode.BadRequest
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
                message: CommentBusinessRuleErrorMessages.NotFound
            );
            return Result.Failure(
                error: exception,
                message: CommentBusinessRuleMessages.NotFound,
                statusCode: (int)HttpStatusCode.NotFound
            );
        }
        return Result.Success();
    }

    public async Task<Result> PostMustExistAsync(string postId)
    {
        var post = await _postReadRepository.GetByIdAsync(postId);

        if (post is null)
        {
            var exception = new BusinessRuleException(
                message: CommentBusinessRuleErrorMessages.PostNotFound
            );
            return Result.Failure(
                error: exception,
                message: CommentBusinessRuleMessages.PostNotFound,
                statusCode: (int)HttpStatusCode.NotFound
            );
        }
        return Result.Success();
    }

    public async Task<Result> CommentCanNotBeDeletedIfHasSubCommentsAsync(string commentId)
    {
        var comment = await _commentReadRepository.GetByIdWithIncludesAsync(
            id: commentId,
            tracking: false,
            includes: x => x.SubComments);

        if (comment != null && comment.SubComments.Any())
        {
            var exception = new BusinessRuleException(
                message: CommentBusinessRuleErrorMessages.CannotDeleteIfHasSubComments
            );
            return Result.Failure(
                error: exception,
                message: CommentBusinessRuleMessages.CannotDeleteIfHasSubComments,
                statusCode: (int)HttpStatusCode.BadRequest
            );
        }
        return Result.Success();
    }
}

