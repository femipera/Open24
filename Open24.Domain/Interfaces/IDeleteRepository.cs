using Open24.Domain.Entities;

namespace Open24.Domain.Interfaces;

public interface IDeleteRepository<TEntity> where TEntity : Base
{
    Task DeleteAsync(TEntity entity);
}
