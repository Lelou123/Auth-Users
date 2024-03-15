using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OrderFlow.Application.ApplicationServices.Policies;
using OrderFlow.Domain.Enums;
using OrderFlow.Domain.Enums.User;
using OrderFlow.Infrastructure.Settings;

namespace OverFlow.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureJwt(configuration)
            .ConfigureCors()
            .ConfigureAuthorization();

        return services;
    }

    private static IServiceCollection ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
    {
        string? jwtSecret = configuration.GetSection("AppSettings:JwtSecret").Value;

        if (jwtSecret is null)
        {
            return services;
        }

        services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            )
            .AddJwtBearer(
                token =>
                {
                    token.RequireHttpsMetadata = true;
                    token.SaveToken = true;

                    JwtSecret.JwtSecretKey = jwtSecret;

                    token.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };
                }
            );

        return services;
    }

    private static IServiceCollection ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(
            options =>
            {
                options.AddPolicy(
                    "CorsPolicy",
                    corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("Content-Disposition")
                );
            }
        );

        return services;
    }

    private static void ConfigureAuthorization(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddTransient<IAuthorizationHandler, UserAuthorizationHandler>();

        services.AddAuthorizationBuilder()
            .AddPolicy(
                PoliciesConstant.AuthUser, policy =>
                {
                    policy.AddRequirements(
                        new UserAuthorizationRequirement(
                            [
                                EnUserRoles.Guest.ToString()
                            ]
                        )
                    );
                }
            );
    }
}