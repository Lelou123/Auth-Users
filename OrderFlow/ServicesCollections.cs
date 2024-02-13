using OrderFlow.Domain.Interfaces.Services;
using OrderFlow.Infra.AutoMapper;

namespace OrderFlow;

public static class ServicesCollections
{
    private static IConfiguration _configuration;
    
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        _configuration = configuration;

        return services;
    }
    
    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddScoped<IMappingService, AutoMapperService>();
    }

}