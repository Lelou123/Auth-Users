using Microsoft.AspNetCore.Identity;

namespace OrderFlow.Domain.Entities.Users;

public class Role : IdentityRole<Guid>
{
    public Role() { }

    public Role(string roleName) : base(roleName) { }

    public virtual ICollection<IdentityUserRole<Guid>> UserRoles { get; set; }

    public virtual ICollection<IdentityRoleClaim<Guid>> RoleClaims { get; set; }
}