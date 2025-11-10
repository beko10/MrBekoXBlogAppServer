using MediatR;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Common.DTOs.Security;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.Constants;
using MrBekoXBlogAppServer.Application.Interfaces.Auth;

namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.Commands.RefreshTokenCommand;

public class RefreshTokenCommandHandler(
    IJwtTokenService jwtTokenService
) : IRequestHandler<RefreshTokenCommandRequest, RefreshTokenCommandResponse>
{
    public async Task<RefreshTokenCommandResponse> Handle(
        RefreshTokenCommandRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var tokenDto = await jwtTokenService.RefreshAsync(
                request.RefreshToken,
                cancellationToken);

            return new RefreshTokenCommandResponse
            {
                Result = ResultData<TokenDto>.Success(tokenDto, AuthOperationResultMessages.RefreshTokenSuccess, 200)
            };
        }
        catch (UnauthorizedAccessException ex)
        {
            // JwtTokenService'den gelen hata mesajlarını kullan
            var errorMessage = ex.Message switch
            {
                "Invalid refresh token." => AuthOperationResultMessages.InvalidRefreshToken,
                "Refresh token expired." => AuthOperationResultMessages.TokenExpired,
                "Token reuse detected." => AuthOperationResultMessages.TokenReuseDetected,
                "User no longer exists." => AuthOperationResultMessages.UserNotFound,
                _ => AuthOperationResultMessages.RefreshTokenFailed
            };

            return new RefreshTokenCommandResponse
            {
                Result = ResultData<TokenDto>.Failure(errorMessage, 401)
            };
        }
        catch (Exception ex)
        {
            return new RefreshTokenCommandResponse
            {
                Result = ResultData<TokenDto>.Failure($"{AuthOperationResultMessages.RefreshTokenFailed}: {ex.Message}", 500)
            };
        }
    }
}

