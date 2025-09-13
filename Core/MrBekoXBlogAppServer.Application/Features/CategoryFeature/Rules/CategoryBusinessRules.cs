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
            return Result.Failure(CategoryBusinessRuleMessages.NameEmpty, statusCode: (int)HttpStatusCode.BadRequest);

        return Result.Success();
    }

    public Result CategoryNameLengthMustBeValid(string categoryName)
    {
        if (categoryName.Length < 2)
            return Result.Failure(CategoryBusinessRuleMessages.NameTooShort, statusCode: (int)HttpStatusCode.BadRequest);

        if (categoryName.Length > 100)
            return Result.Failure(CategoryBusinessRuleMessages.NameTooLong, statusCode: (int)HttpStatusCode.BadRequest);

        return Result.Success();
    }

    public async Task<Result> CategoryNameCannotBeDuplicated(string categoryName, string? excludedId = null)
    {
        var existingCategory = await _categoryReadRepository.GetSingleAsync(c =>
            c.CategoryName == categoryName &&
            (excludedId == null || c.Id != excludedId));

        if (existingCategory is not null)
            return Result.Failure(CategoryBusinessRuleMessages.NameAlreadyExists, statusCode: (int)HttpStatusCode.Conflict);

        return Result.Success();
    }

    public async Task<Result> CategoryMustExist(string categoryId)
    {
        var category = await _categoryReadRepository.GetByIdAsync(categoryId);

        if (category is null)
            return Result.Failure(CategoryBusinessRuleMessages.NotFound, statusCode: (int)HttpStatusCode.NotFound);

        return Result.Success();
    }

    public async Task<Result> CategoryCanNotBeDeletedIfHasPosts(string categoryId)
    {
        var category = await _categoryReadRepository.GetByIdWithIncludesAsync(id:categoryId,tracking:false,includes:x=>x.Posts);
        if (category != null && category.Posts.Any())
            return Result.Failure(CategoryBusinessRuleMessages.HasPosts, statusCode: (int)HttpStatusCode.BadRequest);

        return Result.Success();
    }

    public async Task<Result> CategoryCountMustBeUnderLimit(int limit = 100)
    {
        var count = await _categoryReadRepository.CountAsync();

        if (count >= limit)
            return Result.Failure(CategoryBusinessRuleMessages.CategoryLimitExceeded, statusCode: (int)HttpStatusCode.BadRequest);

        return Result.Success();
    }
}
