using MediatR;

namespace MrBekoXBlogAppServer.Application.Features.PostFeature.Queries.GetByIdPostQuery;

public class GetByIdPostQueryRequest : IRequest<GetByIdPostQueryResponse>
{
    public string Id { get; set; } = null!;
}

