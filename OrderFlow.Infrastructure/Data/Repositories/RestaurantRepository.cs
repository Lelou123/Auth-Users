using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrderFlow.Domain.Entities.RestaurantEntities;
using OrderFlow.Domain.Interfaces.Repositories;
using OrderFlow.Infrastructure.Data.Context;

namespace OrderFlow.Infrastructure.Data.Repositories;

public class RestaurantRepository(
    OrderFlowDbContext context
) : RepositoryBase<Restaurant>(context), IRestaurantRepository;