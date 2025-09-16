using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.Queries.GetByIdContactInfoQuery;

public class GetByIdContactInfoQueryResponse
{
    public ResultData<ResultContactInfoQueryDto> Result { get; set; } = null!;
}
