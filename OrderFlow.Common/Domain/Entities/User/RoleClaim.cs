using Microsoft.AspNetCore.Identity;

namespace OrderFlow.Common.Domain.Entities.User;

public class RoleClaim : IdentityRoleClaim<Guid>
{
    public virtual Role Role { get; set; }
}