using System.Linq.Expressions;

namespace OrderFlow.Domain.Interfaces.Repositories;

public interface IRepositoryBase<TEntity>
    where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        CancellationToken cancellationToken = default
    );

    Task<TEntity?> GetByIdAsync(object id);
    Task InsertAsync(TEntity entity);
    Task InsertRangeAsync(IEnumerable<TEntity> entities);
    Task UpdateAsync(TEntity entity);
    Task HardDeleteAsync(object id);
    Task SoftDeleteAsync(TEntity entity);
    Task<IEnumerable<TEntity>> GetAllIncludingAsync(params Expression<Func<TEntity, object>>[] includeProperties);
}