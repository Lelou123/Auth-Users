using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OrderFlow.Application.ApplicationServices;
using OrderFlow.Domain.Interfaces.Services;

namespace OrderFlow.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly))
            .AddValidatorsFromAssembly(assembly)
            .ConfigureServices();

        return services;
    }

    private static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        return services;
    }
}