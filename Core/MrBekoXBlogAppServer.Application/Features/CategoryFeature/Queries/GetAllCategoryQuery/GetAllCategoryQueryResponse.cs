using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.CategoryFeature.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.CategoryFeature.Queries.GetAllCategoryQuery;

public class GetAllCategoryQueryResponse
{
    public ResultData<IEnumerable<ResultCategoryQueryDto>> Result { get; set; } = null!;
}
