using System.Linq.Expressions;
using OrderFlow.Domain.Abstractions;
using OrderFlow.Domain.Interfaces.Services;

namespace OrderFlow.Domain.Interfaces.Repositories;

public interface IRepositoryBase<TEntity>
    where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAsync(
        bool includeDeleted = false,
        Expression<Func<TEntity, bool>>? filter = null,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<TDestination>> GetAsync<TDestination>(
        IMappingService? mapper = null,
        bool includeDeleted = false,
        Expression<Func<TEntity, bool>>? filter = null,
        CancellationToken cancellationToken = default
    );

    Task<TEntity?> GetByIdAsync(Guid id);

    Task InsertAsync(TEntity entity);

    Task InsertRangeAsync(IEnumerable<TEntity> entities);

    Task UpdateAsync(TEntity entity);

    Task HardDeleteAsync(Guid id);

    Task SoftDeleteAsync(TEntity entity);

    Task<IEnumerable<TEntity>> GetAllIncludingAsync(params Expression<Func<TEntity, object>>[] includeProperties);
}