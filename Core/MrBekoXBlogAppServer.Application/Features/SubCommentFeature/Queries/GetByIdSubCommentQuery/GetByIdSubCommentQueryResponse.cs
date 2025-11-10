using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.SubCommentFeature.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Queries.GetByIdSubCommentQuery;

public class GetByIdSubCommentQueryResponse
{
    public ResultData<ResultSubCommentQueryDto> Result { get; set; } = null!;
}

