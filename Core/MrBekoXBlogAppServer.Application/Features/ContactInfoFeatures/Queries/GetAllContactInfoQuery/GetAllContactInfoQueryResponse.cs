using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.Queries.GetAllContactInfoQuery;

public class GetAllContactInfoQueryResponse
{
    public ResultData<IEnumerable<ResultContactInfoQueryDto>> Result { get; set; } = null!; 
}
