using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.DTOs.LoginUserCommandDTOs;

namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.Queries.GetUserProfileQuery;

public class GetUserProfileQueryResponse
{
    public ResultData<UserDto> Result { get; set; } = null!;
}

