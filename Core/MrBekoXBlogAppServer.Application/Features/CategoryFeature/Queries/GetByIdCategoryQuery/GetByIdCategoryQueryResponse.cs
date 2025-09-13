using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.CategoryFeature.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.CategoryFeature.Queries.GetByIdCategoryQuery;

public class GetByIdCategoryQueryResponse
{
    public ResultData<ResultCategoryQueryDto> Result { get; set; } = null!;
}
