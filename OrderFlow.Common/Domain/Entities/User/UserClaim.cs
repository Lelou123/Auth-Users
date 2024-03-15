using Microsoft.AspNetCore.Identity;

namespace OrderFlow.Common.Domain.Entities.User;

public class UserClaim : IdentityUserClaim<Guid>
{
    public virtual User User { get; set; }
}