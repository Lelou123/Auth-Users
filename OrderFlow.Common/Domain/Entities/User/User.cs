using Microsoft.AspNetCore.Identity;

namespace OrderFlow.Common.Domain.Entities.User;

public class User : IdentityUser<Guid>
{
    public DateTime? UpdatedAt { get; set; } = null;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;

    public virtual ICollection<IdentityUserClaim<Guid>> Claims { get; set; }
    public virtual ICollection<IdentityUserLogin<Guid>> Logins { get; set; }
    public virtual ICollection<IdentityUserToken<Guid>> Tokens { get; set; }
    public virtual ICollection<IdentityUserRole<Guid>> UserRoles { get; set; }
}