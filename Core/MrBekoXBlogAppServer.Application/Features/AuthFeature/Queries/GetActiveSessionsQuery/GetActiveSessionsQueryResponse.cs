using MrBekoXBlogAppServer.Application.Common.DTOs.Security;
using MrBekoXBlogAppServer.Application.Common.Results;

namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.Queries.GetActiveSessionsQuery;

public class GetActiveSessionsQueryResponse
{
    public ResultData<IEnumerable<ActiveSessionDto>> Result { get; set; } = null!;
}

