using Microsoft.AspNetCore.Identity;

namespace OrderFlow.Common.Domain.Entities.User;

public class UserLogin : IdentityUserLogin<Guid>
{
    public virtual User User { get; set; }
}