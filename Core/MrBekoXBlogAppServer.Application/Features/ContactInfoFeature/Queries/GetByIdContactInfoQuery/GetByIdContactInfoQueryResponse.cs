using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.ContactInfoFeature.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.ContactInfoFeature.Queries.GetByIdContactInfoQuery;

public class GetByIdContactInfoQueryResponse
{
    public ResultData<ResultContactInfoQueryDto> Result { get; set; } = null!;
}
