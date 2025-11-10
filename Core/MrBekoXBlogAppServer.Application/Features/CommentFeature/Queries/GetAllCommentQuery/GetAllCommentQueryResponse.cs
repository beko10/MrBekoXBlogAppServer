using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.CommentFeature.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.CommentFeature.Queries.GetAllCommentQuery;

public class GetAllCommentQueryResponse
{
    public ResultData<IEnumerable<ResultCommentQueryDto>> Result { get; set; } = null!;
}

