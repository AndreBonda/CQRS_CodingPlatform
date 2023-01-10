namespace CodingPlatform.Domain.Interfaces.Repositories;

public interface IRepository<TEntity, TPrimaryKey>
{
    Task<TEntity> GetByIdAsync(TPrimaryKey id);
    Task AddAsync(TEntity entity);
    Task SaveAsync();
}