using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MrBekoXBlogAppServer.Application.Behaviors;

namespace MrBekoXBlogAppServer.Application.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // AutoMapper profilleri
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // FluentValidation validatorları
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // MediatR handler + pipeline behavior
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        return services;
    }
}
