using System.ComponentModel.DataAnnotations.Schema;
using OrderFlow.Domain.Abstractions;
using OrderFlow.Domain.Entities.ClientEntities;
using OrderFlow.Domain.Enums;

namespace OrderFlow.Domain.Entities.RestaurantEntities;

public class Order : BaseEntity
{
    public Guid ClientId { get; set; }

    public Guid RestaurantId { get; set; }

    public Guid MenuItemId { get; set; }

    public EnOrderStatus Status { get; set; }
    
    public DateTime ExpectedDate { get; set; }

    [ForeignKey(nameof(ClientId))]
    public virtual Client? Client { get; set; }

    [ForeignKey(nameof(RestaurantId))]
    public virtual Restaurant? Restaurant { get; set; }
    
    [ForeignKey(nameof(MenuItemId))]
    public virtual MenuItem? MenuItem { get; set; }
}