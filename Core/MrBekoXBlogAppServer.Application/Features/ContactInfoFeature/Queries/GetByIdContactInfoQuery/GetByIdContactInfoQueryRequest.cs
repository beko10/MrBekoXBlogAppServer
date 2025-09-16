using MediatR;

namespace MrBekoXBlogAppServer.Application.Features.ContactInfoFeature.Queries.GetByIdContactInfoQuery;

public class GetByIdContactInfoQueryRequest : IRequest<GetByIdContactInfoQueryResponse>
{
    public string? Id { get; set; }
}
