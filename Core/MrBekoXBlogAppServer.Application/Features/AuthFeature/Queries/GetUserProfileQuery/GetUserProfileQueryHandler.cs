using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.DTOs.LoginUserCommandDTOs;
using MrBekoXBlogAppServer.Domain.Entities;
using System.Net;

namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.Queries.GetUserProfileQuery;

public class GetUserProfileQueryHandler(
    UserManager<AppUser> userManager,
    IHttpContextAccessor httpContextAccessor
) : IRequestHandler<GetUserProfileQueryRequest, GetUserProfileQueryResponse>
{
    public async Task<GetUserProfileQueryResponse> Handle(
        GetUserProfileQueryRequest request,
        CancellationToken cancellationToken)
    {
        // JWT token'dan kullanıcı ID'sini al
        var userId = httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return new GetUserProfileQueryResponse
            {
                Result = ResultData<UserDto>.Failure(
                    AuthOperationResultMessages.Unauthorized,
                    (int)HttpStatusCode.Unauthorized
                )
            };
        }

        try
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return new GetUserProfileQueryResponse
                {
                    Result = ResultData<UserDto>.Failure(
                        AuthOperationResultMessages.UserNotFound,
                        (int)HttpStatusCode.NotFound
                    )
                };
            }

            // Kullanıcı rollerini al
            var roles = await userManager.GetRolesAsync(user);

            var userDto = new UserDto
            {
                UserId = user.Id,
                Email = user.Email ?? string.Empty,
                FullName = user.FullName,
                Roles = roles.ToList()
            };

            return new GetUserProfileQueryResponse
            {
                Result = ResultData<UserDto>.Success(
                    userDto,
                    AuthOperationResultMessages.GetUserProfileSuccess,
                    (int)HttpStatusCode.OK
                )
            };
        }
        catch (Exception ex)
        {
            return new GetUserProfileQueryResponse
            {
                Result = ResultData<UserDto>.Failure(
                    $"{AuthOperationResultMessages.GetUserProfileFailed}: {ex.Message}",
                    (int)HttpStatusCode.InternalServerError
                )
            };
        }
    }
}

