using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.CategoryFeature.Constants;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.CategoryRepository;
using System.Net;

namespace MrBekoXBlogAppServer.Application.Features.CategoryFeature.Rules;

public class CategoryBusinessRules(ICategoryReadRepository _categoryReadRepository) : ICategoryBusinessRules
{
    
    public Result CategoryNameCannotBeEmpty(string categoryName)
    {
        if (string.IsNullOrWhiteSpace(categoryName))
        {
            var exception = new BusinessRuleException(
                message: CategoryBusinessRuleErrorMessages.NameEmpty
               );
            return Result.Failure(
                error: exception,
                message: CategoryBusinessRuleMessages.NameEmpty,
                statusCode: (int)HttpStatusCode.BadRequest
             );
        }

        return Result.Success();
    }

    public Result CategoryNameLengthMustBeValid(string categoryName)
    {
        if (categoryName.Length < 2)
        {
            var exception = new BusinessRuleException(
                message: CategoryBusinessRuleErrorMessages.NameTooShort
               );
            return Result.Failure(
                error: exception,
                message:CategoryBusinessRuleMessages.NameTooShort, 
                statusCode: (int)HttpStatusCode.BadRequest
            );
        }

        if (categoryName.Length > 100)
        {
            var exception = new BusinessRuleException(
                message:CategoryBusinessRuleErrorMessages.NameTooLong
               );
            return Result.Failure(
                error: exception,
                message:CategoryBusinessRuleMessages.NameTooLong, 
                statusCode: (int)HttpStatusCode.BadRequest
            );
        }

        return Result.Success();
    }

    public async Task<Result> CategoryNameCannotBeDuplicatedAsync(string categoryName, string? excludedId = null)
    {
        var existingCategory = await _categoryReadRepository.GetSingleAsync(c =>
            c.CategoryName == categoryName &&
            (excludedId == null || c.Id != excludedId));

        if (existingCategory is not null)
        {
            var exception = new BusinessRuleException(
                message:CategoryBusinessRuleErrorMessages.NameAlreadyExists
               );
            return Result.Failure(
                error: exception,
                message:CategoryBusinessRuleMessages.NameAlreadyExists, 
                statusCode: (int)HttpStatusCode.Conflict
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
                message: CategoryBusinessRuleErrorMessages.NotFound
               );
            return Result.Failure(
                error: exception,
                message:CategoryBusinessRuleMessages.NotFound, 
                statusCode: (int)HttpStatusCode.NotFound
            );

        }
        return Result.Success();
    }

    public async Task<Result> CategoryCanNotBeDeletedIfHasPostsAsync(string categoryId)
    {
        var category = await _categoryReadRepository.GetByIdWithIncludesAsync(id:categoryId,tracking:false,includes:x=>x.Posts);
        if (category != null && category.Posts.Any())
        {
            var exception = new BusinessRuleException(
                message: CategoryBusinessRuleErrorMessages.HasPosts
               );
            return Result.Failure(
                error: exception,
                message:CategoryBusinessRuleMessages.HasPosts, 
                statusCode: (int)HttpStatusCode.BadRequest
            );

        }
        return Result.Success();
    }

    public async Task<Result> CategoryCountMustBeUnderLimitAsync(int limit = 100)
    {
        var count = await _categoryReadRepository.CountAsync();

        if (count >= limit)
        {
            var exception = new BusinessRuleException(
                message: CategoryBusinessRuleErrorMessages.CategoryLimitExceeded
               );
            return Result.Failure(
                error: exception,
                message:CategoryBusinessRuleMessages.CategoryLimitExceeded, 
                statusCode: (int)HttpStatusCode.BadRequest
            );

        }
        return Result.Success();
    }
}
