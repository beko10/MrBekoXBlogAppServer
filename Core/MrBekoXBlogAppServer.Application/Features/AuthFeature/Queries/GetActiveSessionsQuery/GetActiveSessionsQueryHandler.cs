using MediatR;
using Microsoft.AspNetCore.Http;
using MrBekoXBlogAppServer.Application.Common.DTOs.Security;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.Constants;
using MrBekoXBlogAppServer.Application.Interfaces.Auth;
using System.Net;

namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.Queries.GetActiveSessionsQuery;

public class GetActiveSessionsQueryHandler(
    ISessionManagementService sessionManagementService,
    IHttpContextAccessor httpContextAccessor
) : IRequestHandler<GetActiveSessionsQueryRequest, GetActiveSessionsQueryResponse>
{
    public async Task<GetActiveSessionsQueryResponse> Handle(
        GetActiveSessionsQueryRequest request,
        CancellationToken cancellationToken)
    {
        // JWT token'dan kullanıcı ID'sini al
        var userId = httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return new GetActiveSessionsQueryResponse
            {
                Result = ResultData<IEnumerable<ActiveSessionDto>>.Failure(
                    AuthOperationResultMessages.Unauthorized,
                    (int)HttpStatusCode.Unauthorized
                )
            };
        }

        try
        {
            var sessions = await sessionManagementService.GetActiveSessionsAsync(userId, cancellationToken);

            if (!sessions.Any())
            {
                return new GetActiveSessionsQueryResponse
                {
                    Result = ResultData<IEnumerable<ActiveSessionDto>>.Failure(
                        AuthOperationResultMessages.NoActiveSessions,
                        (int)HttpStatusCode.NotFound
                    )
                };
            }

            return new GetActiveSessionsQueryResponse
            {
                Result = ResultData<IEnumerable<ActiveSessionDto>>.Success(
                    sessions,
                    AuthOperationResultMessages.GetActiveSessionsSuccess,
                    (int)HttpStatusCode.OK
                )
            };
        }
        catch (Exception ex)
        {
            return new GetActiveSessionsQueryResponse
            {
                Result = ResultData<IEnumerable<ActiveSessionDto>>.Failure(
                    $"{AuthOperationResultMessages.GetActiveSessionsFailed}: {ex.Message}",
                    (int)HttpStatusCode.InternalServerError
                )
            };
        }
    }
}

