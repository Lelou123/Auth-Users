using Microsoft.AspNetCore.Identity;

namespace OrderFlow.Domain.Entities.Users;

public class UserClaim : IdentityUserClaim<Guid>
{
    public virtual User User { get; set; }
}