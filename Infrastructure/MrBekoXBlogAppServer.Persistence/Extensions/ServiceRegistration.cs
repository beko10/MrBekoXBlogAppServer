using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories;
using MrBekoXBlogAppServer.Application.Interfaces.UnitOfWorks;
using MrBekoXBlogAppServer.Domain.Entities;
using MrBekoXBlogAppServer.Persistence.Context;
using MrBekoXBlogAppServer.Persistence.Repositories;
using MrBekoXBlogAppServer.Persistence.UnitOfWorks;
using System.Reflection;

namespace MrBekoXBlogAppServer.Persistence.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistanceServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                //Kullanıcı ayarları
                options.User.RequireUniqueEmail = true;  // Her e-posta sadece 1 kullanıcıya ait olsun
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._"; // Kullanıcı adında izin verilen karakterler

                //Giriş / Hesap ayarları
                options.SignIn.RequireConfirmedEmail = false;         // Email onaylanmadan giriş yapılamaz
                options.SignIn.RequireConfirmedPhoneNumber = false;  // Telefon zorunlu değil
                options.SignIn.RequireConfirmedAccount = false;       // Hesap doğrulaması olmadan giriş engellenir

                //Şifre ayarları
                options.Password.RequiredLength = 8;             // Minimum uzunluk
                options.Password.RequireDigit = true;            // En az 1 rakam
                options.Password.RequireLowercase = true;        // En az 1 küçük harf
                options.Password.RequireUppercase = true;        // En az 1 büyük harf
                options.Password.RequireNonAlphanumeric = true;  // En az 1 özel karakter
                options.Password.RequiredUniqueChars = 3;        // En az 3 farklı karakter içermeli
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            // === Generic repositories ===
            services.AddScoped(typeof(IRepository<>), typeof(EfCoreRepositories<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(EfCoreWriteRepository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(EfCoreReadRepository<>));

            // === Entity-specific repositories otomatik ekle ===
            services.Scan(scan => scan
                .FromAssemblies(Assembly.GetExecutingAssembly())
                .AddClasses(classes => classes.Where(c => c.Name.StartsWith("EfCore")))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );

            // === UnitOfWork ===
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
