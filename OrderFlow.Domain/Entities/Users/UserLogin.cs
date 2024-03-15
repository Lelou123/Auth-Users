using Microsoft.AspNetCore.Identity;

namespace OrderFlow.Domain.Entities.Users;

public class UserLogin : IdentityUserLogin<Guid>
{
    public virtual User User { get; set; }
}