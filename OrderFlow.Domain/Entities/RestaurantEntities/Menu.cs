using System.ComponentModel.DataAnnotations.Schema;
using OrderFlow.Domain.Abstractions;

namespace OrderFlow.Domain.Entities.RestaurantEntities;

public class Menu(
    string name,
    string description,
    Guid restaurantId
)
    : BaseEntity
{
    public string Name { get; set; } = name;

    public string Description { get; set; } = description;

    public Guid RestaurantId { get; set; } = restaurantId;
    
    public virtual ICollection<MenuItem>? Items { get; set; }

    [ForeignKey(nameof(RestaurantId))]
    public virtual Restaurant? Restaurant { get; set; }
}