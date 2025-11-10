using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.SubCommentFeature.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Queries.GetAllSubCommentQuery;

public class GetAllSubCommentQueryResponse
{
    public ResultData<IEnumerable<ResultSubCommentQueryDto>> Result { get; set; } = null!;
}

