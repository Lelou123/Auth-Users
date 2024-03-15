using System.ComponentModel.DataAnnotations.Schema;
using OrderFlow.Domain.Abstractions;
using OrderFlow.Domain.Entities.Users;

namespace OrderFlow.Domain.Entities.ClientEntities;

public class Client(Guid userId,
    string document
) : BaseEntity
{
    public string Document { get; set; } = document;

    public Guid UserId { get; set; } = userId;

    [ForeignKey(nameof(UserId))]
    public virtual User? User { get; set; }
}