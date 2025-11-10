using MediatR;

namespace MrBekoXBlogAppServer.Application.Features.CommentFeature.Queries.GetByIdCommentQuery;

public class GetByIdCommentQueryRequest : IRequest<GetByIdCommentQueryResponse>
{
    public string Id { get; set; } = null!;
}

