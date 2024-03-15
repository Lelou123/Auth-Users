using Microsoft.AspNetCore.Identity;

namespace OrderFlow.Common.Domain.Entities.User;

public class UserRole : IdentityUserRole<Guid>
{
    public virtual User User { get; set; }
    public virtual Role Role { get; set; }
}