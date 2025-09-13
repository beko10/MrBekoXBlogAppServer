using MrBekoXBlogAppServer.Application.Common.Results;

namespace MrBekoXBlogAppServer.Application.Features.CategoryFeature.Rules;

public interface ICategoryBusinessRules
{
    Result CategoryNameCannotBeEmpty(string categoryName);

    Result CategoryNameLengthMustBeValid(string categoryName);

    Task<Result> CategoryNameCannotBeDuplicated(string categoryName, string? excludedId = null);

    Task<Result> CategoryMustExist(string categoryId);

    Task<Result> CategoryCanNotBeDeletedIfHasPosts(string categoryId);

    Task<Result> CategoryCountMustBeUnderLimit(int limit = 100);
}
