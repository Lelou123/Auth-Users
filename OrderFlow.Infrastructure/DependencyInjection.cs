using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderFlow.Common.Domain.Entities.User;
using OrderFlow.Domain.Interfaces.Services;
using OrderFlow.Infrastructure.AutoMapper;
using OrderFlow.Infrastructure.Data.Context;

namespace OrderFlow.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureAutoMapper()
            .ConfigureDataBase(configuration)
            .ConfigureIdentity();

        return services;
    }

    private static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddScoped<IMappingService, AutoMapperService>();

        return services;
    }

    private static IServiceCollection ConfigureDataBase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OrderFlowDbContext>(
            options => options.UseLazyLoadingProxies()
                .UseNpgsql(configuration.GetConnectionString("ConnectionPG"))
        );

        return services;
        //services.AddTransient<IWeatherForecastRepository, WeatherForecastRepository>();
    }

    private static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<OrderFlowDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(
            options =>
            {
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