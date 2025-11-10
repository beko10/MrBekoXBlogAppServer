using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.PostFeature.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.PostFeature.Queries.GetAllPostQuery;

public class GetAllPostQueryResponse
{
    public ResultData<IEnumerable<ResultPostQueryDto>> Result { get; set; } = null!;
}

