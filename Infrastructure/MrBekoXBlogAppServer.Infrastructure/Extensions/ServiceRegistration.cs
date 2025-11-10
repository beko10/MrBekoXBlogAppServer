using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MrBekoXBlogAppServer.Application.Interfaces.Auth;
using MrBekoXBlogAppServer.Application.Interfaces.Email;
using MrBekoXBlogAppServer.Infrastructure.Auth;
using MrBekoXBlogAppServer.Infrastructure.Auth.Options;
using MrBekoXBlogAppServer.Infrastructure.Email;
using MrBekoXBlogAppServer.Infrastructure.Email.Options;
using System.Text;

namespace MrBekoXBlogAppServer.Infrastructure.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // JWT Options'ı yapılandır
        services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.SectionName));

        // Email Options'ı yapılandır
        services.Configure<EmailOptions>(
            configuration.GetSection(EmailOptions.SectionName));

        // JWT Options'ı al
        var jwtSection = configuration.GetSection(JwtOptions.SectionName);
        var jwtOptions = new JwtOptions();
        jwtSection.Bind(jwtOptions);

        if (string.IsNullOrEmpty(jwtOptions.SecretKey))
            throw new InvalidOperationException("JWT SecretKey is not configured");

        // JWT Authentication ekle
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudiences = jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
                ClockSkew = TimeSpan.Zero, // Production'da sıfır olmalı
            };

            // Token doğrulama event'leri
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    // Logging eklenebilir
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    // Token doğrulandıktan sonra ek kontroller
                    return Task.CompletedTask;
                }
            };

        });

        // Memory Cache ekle (Token Blacklist için)
        services.AddMemoryCache();

        // JWT Token Service
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        // Token Blacklist Service
        services.AddScoped<ITokenBlacklistService, TokenBlacklistService>();

        // Session Management Service
        services.AddScoped<ISessionManagementService, SessionManagementService>();

        // Email Service
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}

