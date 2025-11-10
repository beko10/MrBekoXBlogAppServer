using MediatR;

namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.Queries.GetActiveSessionsQuery;

public class GetActiveSessionsQueryRequest : IRequest<GetActiveSessionsQueryResponse>
{
    // UserId, JWT token'dan alınacak (Claims'den)
}

