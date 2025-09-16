using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MrBekoXBlogAppServer.Application.Behaviors;
using MrBekoXBlogAppServer.Application.Features.CategoryFeature.Rules;
using MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.Rules;
using System.Reflection;

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

        // Business Rules
        services.AddScoped<ICategoryBusinessRules, CategoryBusinessRules>();
        services.AddScoped<IContactInfoBusinessRules, ContactInfoBusinessRules>();

        return services;
    }
}
