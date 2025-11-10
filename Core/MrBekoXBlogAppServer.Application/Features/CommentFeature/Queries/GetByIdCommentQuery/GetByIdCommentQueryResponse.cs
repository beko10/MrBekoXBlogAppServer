using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.CommentFeature.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.CommentFeature.Queries.GetByIdCommentQuery;

public class GetByIdCommentQueryResponse
{
    public ResultData<ResultCommentQueryDto> Result { get; set; } = null!;
}

