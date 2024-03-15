using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OrderFlow.Domain.Abstractions;
using OrderFlow.Domain.Interfaces.Repositories;
using OrderFlow.Domain.Interfaces.Services;

namespace OrderFlow.Infrastructure.Data.Repositories;

public abstract class RepositoryBase<TEntity>(
    DbContext context
) : IRepositoryBase<TEntity>
    where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public virtual async Task<IEnumerable<TEntity>> GetAsync(
        bool includeDeleted = false,
        Expression<Func<TEntity, bool>>? filter = null,
        CancellationToken cancellationToken = default
    )
    {
        Expression<Func<TEntity, bool>> defaultFilter = entity => !entity.IsDeleted && entity.IsActive;

        if (includeDeleted)
        {
            defaultFilter = entity => !entity.IsActive && entity.IsDeleted;
        }

        if (filter != null)
        {
            defaultFilter = Expression.Lambda<Func<TEntity, bool>>(
                Expression.AndAlso(defaultFilter.Body, filter.Body),
                filter.Parameters
            );
        }

        IQueryable<TEntity> query = _dbSet;

        query = query.Where(defaultFilter);

        return await query.ToListAsync(cancellationToken);
    }


    public virtual async Task<IEnumerable<TDestination>> GetAsync<TDestination>(
        IMappingService? mapper = null,
        bool includeDeleted = false,
        Expression<Func<TEntity, bool>>? filter = null,
        CancellationToken cancellationToken = default
    )
    {
        Expression<Func<TEntity, bool>> defaultFilter = entity => !entity.IsDeleted && entity.IsActive;

        if (mapper is null)
        {
            throw new Exception("Error while mapping entity");
        }
        
        if (includeDeleted)
        {
            defaultFilter = entity => !entity.IsActive && entity.IsDeleted;
        }

        if (filter != null)
        {
            defaultFilter = Expression.Lambda<Func<TEntity, bool>>(
                Expression.AndAlso(defaultFilter.Body, filter.Body),
                filter.Parameters
            );
        }

        IQueryable<TEntity> query = _dbSet;

        query = query.Where(defaultFilter);
        
        List<TEntity> entities = await query.ToListAsync(cancellationToken);

        return mapper.Map<IEnumerable<TDestination>>(entities);
    }


    public virtual async Task<TEntity?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);


    public virtual async Task InsertAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await context.SaveChangesAsync();
    }


    public virtual async Task InsertRangeAsync(IEnumerable<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);
        await context.SaveChangesAsync();
    }


    public virtual async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Attach(entity);

        context.Entry(entity).State = EntityState.Modified;

        var propertyUpdatedAt = entity.GetType().GetProperty("UpdatedAt");
        propertyUpdatedAt?.SetValue(entity, DateTime.UtcNow);

        await context.SaveChangesAsync();
    }


    public virtual async Task HardDeleteAsync(Guid id)
    {
        var entityToDelete = await _dbSet.FindAsync(id);

        if (entityToDelete is not null)
        {
            _dbSet.Remove(entityToDelete);
            await context.SaveChangesAsync();
        }
    }


    public virtual async Task SoftDeleteAsync(TEntity entity)
    {
        if (context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }

        var propertyUpdatedAt = entity.GetType().GetProperty("UpdatedAt");
        propertyUpdatedAt?.SetValue(entity, DateTime.UtcNow);

        var propertyIsDeleted = entity.GetType().GetProperty("IsDeleted");
        propertyIsDeleted?.SetValue(entity, true);

        var propertyIsActive = entity.GetType().GetProperty("IsActive");
        propertyIsActive?.SetValue(entity, false);

        context.Entry(entity).State = EntityState.Modified;

        await context.SaveChangesAsync();
    }


    public async Task<IEnumerable<TEntity>> GetAllIncludingAsync(
        params Expression<Func<TEntity, object>>[]? includeProperties
    )
    {
        IQueryable<TEntity> query = context.Set<TEntity>();

        if (includeProperties != null)
        {
            query = includeProperties.Aggregate(
                query,
                (current, includeProperty) => current.Include(includeProperty)
            );
        }

        return await query.ToListAsync();
    }
}