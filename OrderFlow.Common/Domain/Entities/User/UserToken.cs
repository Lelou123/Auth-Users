using Microsoft.AspNetCore.Identity;

namespace OrderFlow.Common.Domain.Entities.User;

public class UserToken : IdentityUserToken<Guid>
{
    public virtual User User { get; set; }
}