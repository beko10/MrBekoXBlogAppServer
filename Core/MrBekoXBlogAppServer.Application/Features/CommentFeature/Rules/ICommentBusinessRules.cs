using MrBekoXBlogAppServer.Application.Common.Results;

namespace MrBekoXBlogAppServer.Application.Features.CommentFeature.Rules;

public interface ICommentBusinessRules
{
    Result CommentContentCannotBeEmpty(string content);

    Result CommentContentLengthMustBeValid(string content);

    Task<Result> CommentMustExistAsync(string commentId);

    Task<Result> PostMustExistAsync(string postId);

    Task<Result> CommentCanNotBeDeletedIfHasSubCommentsAsync(string commentId);
}

