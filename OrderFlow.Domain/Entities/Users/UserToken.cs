using Microsoft.AspNetCore.Identity;

namespace OrderFlow.Domain.Entities.Users;

public class UserToken : IdentityUserToken<Guid>
{
    public virtual User User { get; set; }
}