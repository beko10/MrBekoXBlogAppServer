using MediatR;

namespace MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Queries.GetByIdSubCommentQuery;

public class GetByIdSubCommentQueryRequest : IRequest<GetByIdSubCommentQueryResponse>
{
    public string Id { get; set; } = null!;
}

