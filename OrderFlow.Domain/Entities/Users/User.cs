using Microsoft.AspNetCore.Identity;
using OrderFlow.Domain.Entities.Address;
using OrderFlow.Domain.Entities.ClientEntities;
using OrderFlow.Domain.Entities.RestaurantEntities;

namespace OrderFlow.Domain.Entities.Users;

public class User(
    string fullName
) : IdentityUser<Guid>
{
    public string FullName { get; init; } = fullName;

    public DateTime BirthDate { get; set; }

    public DateTime? UpdatedAt { get; init; }

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;

    public Guid AddressId { get; init; }

    public virtual Client? Client { get; init; }

    public virtual Restaurant? Restaurant { get; init; }

    public virtual UserAddress? Address { get; init; }

    public virtual ICollection<IdentityUserClaim<Guid>>? Claims { get; init; }

    public virtual ICollection<IdentityUserLogin<Guid>>? Logins { get; init; }

    public virtual ICollection<IdentityUserToken<Guid>>? Tokens { get; init; }

    public virtual ICollection<IdentityUserRole<Guid>>? UserRoles { get; init; }
}