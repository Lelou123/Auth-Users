using System.ComponentModel.DataAnnotations.Schema;
using OrderFlow.Domain.Abstractions;
using OrderFlow.Domain.Entities.Users;
using OrderFlow.Domain.Enums;

namespace OrderFlow.Domain.Entities.RestaurantEntities;

public class Restaurant(
    string name,
    EnRestaurantType type,
    Guid userId,
    string document
) : BaseEntity
{
    public string Name { get; init; } = name;
    
    public string Document { get; init; } = document;

    public EnRestaurantType Type { get; init; } = type;

    public Guid UserId { get; init; } = userId;

    [ForeignKey(nameof(UserId))]
    public virtual User? User { get; init; }
    
    public virtual ICollection<Menu>? Menus { get; set; }
}