using System.ComponentModel.DataAnnotations.Schema;
using OrderFlow.Domain.Abstractions;
using OrderFlow.Domain.Entities.Users;
using OrderFlow.Domain.Enums;

namespace OrderFlow.Domain.Entities.Address;

public class UserAddress(
    EnCountry country,
    string city,
    string street,
    Guid userId
) : BaseEntity
{
    public EnCountry Country { get; init; } = country;

    public string City { get; init; } = city;

    public string Street { get; init; } = street;

    public Guid UserId { get; init; } = userId;

    [ForeignKey(nameof(UserId))]
    public virtual User? User { get; init; }
}