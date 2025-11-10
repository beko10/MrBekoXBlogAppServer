using MrBekoXBlogAppServer.Application.Common.Results;

namespace MrBekoXBlogAppServer.Application.Features.PostFeature.Rules;

public interface IPostBusinessRules
{
    Result PostTitleCannotBeEmpty(string title);

    Result PostTitleLengthMustBeValid(string title);

    Result PostContentCannotBeEmpty(string content);

    Result PostContentLengthMustBeValid(string content);

    Result PostAuthorCannotBeEmpty(string author);

    Result PostAuthorLengthMustBeValid(string author);

    Task<Result> PostMustExistAsync(string postId);

    Task<Result> CategoryMustExistAsync(string categoryId);

    Task<Result> PostCanNotBeDeletedIfHasCommentsAsync(string postId);
}

