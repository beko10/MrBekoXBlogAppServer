using MrBekoXBlogAppServer.Application.Common.Results;

namespace MrBekoXBlogAppServer.Application.Features.CategoryFeature.Rules;

public interface ICategoryBusinessRules
{
    Result CategoryNameCannotBeEmpty(string categoryName);

    Result CategoryNameLengthMustBeValid(string categoryName);

    Task<Result> CategoryNameCannotBeDuplicatedAsync(string categoryName, string? excludedId = null);

    Task<Result> CategoryMustExistAsync(string categoryId);

    Task<Result> CategoryCanNotBeDeletedIfHasPostsAsync(string categoryId);

    Task<Result> CategoryCountMustBeUnderLimitAsync(int limit = 100);
}
