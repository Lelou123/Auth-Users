using Microsoft.EntityFrameworkCore;
using OrderFlow.Common.Domain.Entities.User;
using OrderFlow.Domain.Enums.User;
using OrderFlow.Infrastructure.Data.Context;

namespace OrderFlow.Extensions;

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
            new List<Role>
            {
                new(EnUserRoles.Guest.ToString()),
                new(EnUserRoles.Admin.ToString()),
                new(EnUserRoles.SuperAdmin.ToString())
            }
        );

        dbContext.SaveChanges();
    }
}