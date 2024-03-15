using System.ComponentModel.DataAnnotations.Schema;
using OrderFlow.Domain.Abstractions;

namespace OrderFlow.Domain.Entities.RestaurantEntities;

public class MenuItem(
    double price,
    string name,
    double weight,
    Guid menuId
) : BaseEntity
{
    public string Name { get; set; } = name;

    public double Price { get; set; } = price;

    public double Weight { get; set; } = weight;

    public Guid MenuId { get; set; } = menuId;
    
    [ForeignKey(nameof(MenuId))]
    public virtual Menu? Client { get; set; }
}