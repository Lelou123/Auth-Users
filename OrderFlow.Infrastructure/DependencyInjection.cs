using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderFlow.Domain.Entities.Users;
using OrderFlow.Domain.Interfaces.Repositories;
using OrderFlow.Domain.Interfaces.Services;
using OrderFlow.Infrastructure.AutoMapper;
using OrderFlow.Infrastructure.Data.Context;
using OrderFlow.Infrastructure.Data.Repositories;
using OrderFlow.Infrastructure.Settings;

namespace OrderFlow.Infrastructure;

public static class DependencyInjection
{
    private static IConfiguration _configuration = null!;

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        _configuration = configuration;

        services.ConfigureAutoMapper()
            .ConfigureDataBase()
            .ConfigureKeys()
            .ConfigureIdentity();

        return services;
    }

    private static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddScoped<IMappingService, AutoMapperService>();

        return services;
    }

    private static IServiceCollection ConfigureDataBase(this IServiceCollection services)
    {
        services.AddDbContext<IApplicationDbContext, OrderFlowDbContext>(
            options => options.UseLazyLoadingProxies()
                .UseNpgsql(_configuration.GetConnectionString("ConnectionPG"))
        );

        services.AddTransient<IClientRepository, ClientRepository>();
        services.AddTransient<IRestaurantRepository, RestaurantRepository>();

        return services;
    }

    private static IServiceCollection ConfigureKeys(this IServiceCollection services)
    {
        services.Configure<EmailSettings>(_configuration.GetSection("EmailSettings"));

        services.Configure<JwtSecret>(_configuration.GetSection("JwtSecret"));

        return services;
    }

    private static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<OrderFlowDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(
            options => {
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;

                options.SignIn.RequireConfirmedEmail = true;

                options.User.RequireUniqueEmail = true;

                options.Lockout.MaxFailedAccessAttempts = 3;
            }
        );

        //Configure token lifespan
        services.Configure<DataProtectionTokenProviderOptions>(
            o =>
                o.TokenLifespan = TimeSpan.FromHours(1)
        );
    }
}