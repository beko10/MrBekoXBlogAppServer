using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.ContactInfoFeature.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.ContactInfoFeature.Queries.GetAllContactInfoQuery;

public class GetAllContactInfoQueryResponse
{
    public ResultData<IEnumerable<ResultContactInfoQueryDto>> Result { get; set; } = null!; 
}
