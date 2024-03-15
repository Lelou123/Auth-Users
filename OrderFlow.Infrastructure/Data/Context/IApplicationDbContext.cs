using Microsoft.EntityFrameworkCore;
using OrderFlow.Domain.Entities.Address;
using OrderFlow.Domain.Entities.ClientEntities;
using OrderFlow.Domain.Entities.RestaurantEntities;

namespace OrderFlow.Infrastructure.Data.Context;

public interface IApplicationDbContext
{
    public DbSet<UserAddress> UserAddresses { get; set; }

    public DbSet<Restaurant> Restaurants { get; set; }

    public DbSet<Client> Clients { get; set; }

    public DbSet<Order> Order { get; set; }

    public DbSet<Menu> Menus { get; set; }

    public DbSet<MenuItem> MenuItems { get; set; }
}