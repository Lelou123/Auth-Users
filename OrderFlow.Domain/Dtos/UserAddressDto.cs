using OrderFlow.Domain.Enums;

namespace OrderFlow.Domain.Dtos;

public class UserAddressDto(
    EnCountry country,
    Guid userId,
    string street,
    string city
)
{
    public EnCountry Country { get; init; } = country;

    public string City { get; init; } = city;

    public string Street { get; init; } = street;

    public Guid UserId { get; init; } = userId;
}