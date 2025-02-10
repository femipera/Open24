using Open24.Domain.Entities;

namespace Open24.Domain.Interfaces;

public interface IUpdateRepository<TEntity> where TEntity : Base
{
    Task<TEntity> UpdateAsync(TEntity entity);
}
