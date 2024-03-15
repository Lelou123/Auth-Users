using OrderFlow.Domain.Entities.ClientEntities;
using OrderFlow.Domain.Interfaces.Repositories;
using OrderFlow.Infrastructure.Data.Context;

namespace OrderFlow.Infrastructure.Data.Repositories;

public class ClientRepository(
    OrderFlowDbContext context
) : RepositoryBase<Client>(context), IClientRepository;