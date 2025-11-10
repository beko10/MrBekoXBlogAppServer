using MediatR;

namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.Queries.GetUserProfileQuery;

public class GetUserProfileQueryRequest : IRequest<GetUserProfileQueryResponse>
{
    // UserId, JWT token'dan alınacak (Claims'den)
}

