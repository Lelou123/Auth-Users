using Microsoft.AspNetCore.Identity;

namespace OrderFlow.Domain.Entities.Users;

public class RoleClaim : IdentityRoleClaim<Guid>
{
    public virtual Role Role { get; set; }
}