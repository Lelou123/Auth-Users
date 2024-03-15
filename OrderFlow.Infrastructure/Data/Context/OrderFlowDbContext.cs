using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrderFlow.Common.Domain.Entities.User;
using OrderFlow.Infrastructure.Data.Mapping;

namespace OrderFlow.Infrastructure.Data.Context;

public class OrderFlowDbContext(
    DbContextOptions<OrderFlowDbContext> options
) : IdentityDbContext<User, Role, Guid>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        try
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(new UserMap().Configure);
            builder.Entity<Role>(new RoleMap().Configure);
            builder.Entity<IdentityUserClaim<Guid>>(new UserClaimsMap().Configure);
            builder.Entity<IdentityUserLogin<Guid>>(new UserLoginMap().Configure);
            builder.Entity<IdentityUserRole<Guid>>(new UserRoleMap().Configure);
            builder.Entity<IdentityUserToken<Guid>>(new UserTokenMap().Configure);
            builder.Entity<IdentityRoleClaim<Guid>>(new RoleClaimsMap().Configure);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error on Creating Model: {e.Message}");

            throw;
        }
    }
}