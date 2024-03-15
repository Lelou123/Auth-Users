using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderFlow.Domain.Entities.Users;
using OrderFlow.Domain.Enums.Users;
using OrderFlow.Infrastructure.Data.Context;

namespace OverFlow.Presentation.Extensions;

public static class MigrationExtension
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<OrderFlowDbContext>();
        dbContext.Database.Migrate();

        if (dbContext.Roles.Any())
        {
            return;
        }

        dbContext.Roles.AddRange(
            new List<Role> {
                new(EnUserRoles.Guest.ToString()),
                new(EnUserRoles.Admin.ToString()),
                new(EnUserRoles.SuperAdmin.ToString())
            }
        );

        dbContext.SaveChanges();
    }
}