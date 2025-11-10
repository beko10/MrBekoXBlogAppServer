using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.PostFeature.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.PostFeature.Queries.GetByIdPostQuery;

public class GetByIdPostQueryResponse
{
    public ResultData<ResultPostQueryDto> Result { get; set; } = null!;
}

