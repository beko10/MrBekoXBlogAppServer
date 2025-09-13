using MediatR;

namespace MrBekoXBlogAppServer.Application.Features.CategoryFeature.Queries.GetByIdCategoryQuery;

public class GetByIdCategoryQueryRequest : IRequest<GetByIdCategoryQueryResponse>
{
    public string? Id { get; set; }
}
