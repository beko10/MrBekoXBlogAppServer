using MrBekoXBlogAppServer.Application.Common.Results;

namespace MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Rules;

public interface ISubCommentBusinessRules
{
    Result SubCommentContentCannotBeEmpty(string content);

    Result SubCommentContentLengthMustBeValid(string content);

    Task<Result> SubCommentMustExistAsync(string subCommentId);

    Task<Result> CommentMustExistAsync(string commentId);
}

