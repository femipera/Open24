using Open24.Domain.Entities;

namespace Open24.Domain.Interfaces;

public interface ICreateRepository<TEntity> where TEntity : Base
{
    Task<TEntity> CreateAsync(TEntity entity);
}
