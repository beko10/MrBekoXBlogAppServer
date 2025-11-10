using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.PostFeature.Constants;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.CategoryRepository;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.PostRepository;
using System.Net;

namespace MrBekoXBlogAppServer.Application.Features.PostFeature.Rules;

public class PostBusinessRules(
    IPostReadRepository _postReadRepository,
    ICategoryReadRepository _categoryReadRepository) : IPostBusinessRules
{
    public Result PostTitleCannotBeEmpty(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            var exception = new BusinessRuleException(
                message: PostBusinessRuleErrorMessages.TitleEmpty
            );
            return Result.Failure(
                error: exception,
                message: PostBusinessRuleMessages.TitleEmpty,
                statusCode: (int)HttpStatusCode.BadRequest
            );
        }

        return Result.Success();
    }

    public Result PostTitleLengthMustBeValid(string title)
    {
        if (title.Length < 3)
        {
            var exception = new BusinessRuleException(
                message: PostBusinessRuleErrorMessages.TitleTooShort
            );
            return Result.Failure(
                error: exception,
                message: PostBusinessRuleMessages.TitleTooShort,
                statusCode: (int)HttpStatusCode.BadRequest
            );
        }

        if (title.Length > 200)
        {
            var exception = new BusinessRuleException(
                message: PostBusinessRuleErrorMessages.TitleTooLong
            );
            return Result.Failure(
                error: exception,
                message: PostBusinessRuleMessages.TitleTooLong,
                statusCode: (int)HttpStatusCode.BadRequest
            );
        }

        return Result.Success();
    }

    public Result PostContentCannotBeEmpty(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            var exception = new BusinessRuleException(
                message: PostBusinessRuleErrorMessages.ContentEmpty
            );
            return Result.Failure(
                error: exception,
                message: PostBusinessRuleMessages.ContentEmpty,
                statusCode: (int)HttpStatusCode.BadRequest
            );
        }

        return Result.Success();
    }

    public Result PostContentLengthMustBeValid(string content)
    {
        if (content.Length < 10)
        {
            var exception = new BusinessRuleException(
                message: PostBusinessRuleErrorMessages.ContentTooShort
            );
            return Result.Failure(
                error: exception,
                message: PostBusinessRuleMessages.ContentTooShort,
                statusCode: (int)HttpStatusCode.BadRequest
            );
        }

        return Result.Success();
    }

    public Result PostAuthorCannotBeEmpty(string author)
    {
        if (string.IsNullOrWhiteSpace(author))
        {
            var exception = new BusinessRuleException(
                message: PostBusinessRuleErrorMessages.AuthorEmpty
            );
            return Result.Failure(
                error: exception,
                message: PostBusinessRuleMessages.AuthorEmpty,
                statusCode: (int)HttpStatusCode.BadRequest
            );
        }

        return Result.Success();
    }

    public Result PostAuthorLengthMustBeValid(string author)
    {
        if (author.Length < 2)
        {
            var exception = new BusinessRuleException(
                message: PostBusinessRuleErrorMessages.AuthorTooShort
            );
            return Result.Failure(
                error: exception,
                message: PostBusinessRuleMessages.AuthorTooShort,
                statusCode: (int)HttpStatusCode.BadRequest
            );
        }

        if (author.Length > 100)
        {
            var exception = new BusinessRuleException(
                message: PostBusinessRuleErrorMessages.AuthorTooLong
            );
            return Result.Failure(
                error: exception,
                message: PostBusinessRuleMessages.AuthorTooLong,
                statusCode: (int)HttpStatusCode.BadRequest
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
                message: PostBusinessRuleErrorMessages.NotFound
            );
            return Result.Failure(
                error: exception,
                message: PostBusinessRuleMessages.NotFound,
                statusCode: (int)HttpStatusCode.NotFound
            );
        }
        return Result.Success();
    }

    public async Task<Result> CategoryMustExistAsync(string categoryId)
    {
        var category = await _categoryReadRepository.GetByIdAsync(categoryId);

        if (category is null)
        {
            var exception = new BusinessRuleException(
                message: PostBusinessRuleErrorMessages.CategoryNotFound
            );
            return Result.Failure(
                error: exception,
                message: PostBusinessRuleMessages.CategoryNotFound,
                statusCode: (int)HttpStatusCode.NotFound
            );
        }
        return Result.Success();
    }

    public async Task<Result> PostCanNotBeDeletedIfHasCommentsAsync(string postId)
    {
        var post = await _postReadRepository.GetByIdWithIncludesAsync(
            id: postId,
            tracking: false,
            includes: x => x.Comments);

        if (post != null && post.Comments.Any())
        {
            var exception = new BusinessRuleException(
                message: PostBusinessRuleErrorMessages.CannotDeleteIfHasComments
            );
            return Result.Failure(
                error: exception,
                message: PostBusinessRuleMessages.CannotDeleteIfHasComments,
                statusCode: (int)HttpStatusCode.BadRequest
            );
        }
        return Result.Success();
    }
}

