using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MrBekoXBlogAppServer.Application.Common.BusinessRuleEngines;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.DTOs.LoginUserCommandDTOs;
using MrBekoXBlogAppServer.Application.Features.AuthFeature.Rules;
using MrBekoXBlogAppServer.Application.Interfaces.Auth;
using MrBekoXBlogAppServer.Domain.Entities;

namespace MrBekoXBlogAppServer.Application.Features.AuthFeature.Commands.LoginUserCommand;

public class LoginUserCommandHandler(
    UserManager<AppUser> userManager,
    IJwtTokenService jwtTokenService,
    IHttpContextAccessor httpContextAccessor,
    IAuthBusinessRules authBusinessRules
    ) : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
{
    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        // 1. Kullanıcıyı bul
        var user = await userManager.FindByEmailAsync(request.Email ?? string.Empty);
        if (user == null)
        {
            return new LoginUserCommandResponse
            {
                Result = ResultData<LoginUserCommandResponseDto>.Failure(
                    AuthOperationResultMessages.InvalidEmailOrPassword,
                    401
                )
            };
        }

        // 2. Business Rules: Kullanıcı durumu kontrolleri (şifre kontrolü hariç)
        var userStatusRuleResult = await BusinessRuleEngine.RunAsync(
            () => authBusinessRules.UserMustBeActiveAsync(user),
            () => authBusinessRules.UserMustNotBeLockedOutAsync(user),
            () => authBusinessRules.CheckLoginAttemptsAsync(user)
        );

        // 3. Kullanıcı durumu kontrolleri başarısız olduysa
        if (!userStatusRuleResult.IsSuccess)
        {
            return new LoginUserCommandResponse
            {
                Result = ResultData<LoginUserCommandResponseDto>.Failure(
                    userStatusRuleResult.Message,
                    userStatusRuleResult.StatusCode
                )
            };
        }

        // 4. Business Rules: Şifre doğrulaması (başarısız olursa AccessFailedAsync çağrılacak)
        var passwordResult = await authBusinessRules.PasswordMustBeValidAsync(user, request.Password ?? string.Empty);
        if (!passwordResult.IsSuccess)
        {
            // Başarısız giriş denemesi kaydet
            await userManager.AccessFailedAsync(user);

            return new LoginUserCommandResponse
            {
                Result = ResultData<LoginUserCommandResponseDto>.Failure(
                    AuthOperationResultMessages.InvalidEmailOrPassword,
                    401
                )
            };
        }

        // Başarılı giriş - AccessFailedCount'u sıfırla
        await userManager.ResetAccessFailedCountAsync(user);

        // 7. Device bilgilerini HttpContext'ten otomatik al
        var httpContext = httpContextAccessor.HttpContext;
        var userAgent = httpContext?.Request.Headers["User-Agent"].ToString() ?? "Unknown";
        var ipAddress = httpContext?.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        var deviceInfo = userAgent; // User-Agent'ı device info olarak kullan

        // 8. JWT token üret
        var tokenDto = await jwtTokenService.CreateAccessTokenAsync(
            user.Id,
            deviceInfo,
            ipAddress,
            userAgent,
            cancellationToken);

        // 9. Kullanıcı rollerini al
        var roles = await userManager.GetRolesAsync(user);

        // 10. UserDto oluştur
        var userDto = new UserDto
        {
            UserId = user.Id,
            Email = user.Email ?? string.Empty,
            FullName = user.FullName,
            Roles = roles.ToList()
        };

        // 11. Response oluştur
        var responseDto = new LoginUserCommandResponseDto
        {
            AccessToken = tokenDto.AccessToken.Token,
            AccessTokenExpiresAt = tokenDto.AccessToken.ExpirationAt,
            RefreshToken = tokenDto.RefreshToken.Token,
            RefreshTokenExpiresAt = tokenDto.RefreshToken.ExpiresAt,
            Jti = tokenDto.AccessToken.Jti,
            UserDto = userDto,
            DeviceInfo = deviceInfo,
            IpAddress = ipAddress,
            Message = AuthOperationResultMessages.LoginSuccess
        };

        return new LoginUserCommandResponse
        {
            Result = ResultData<LoginUserCommandResponseDto>.Success(responseDto, AuthOperationResultMessages.LoginSuccess, 200)
        };
    }
}
