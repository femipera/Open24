using Open24.Domain.Entities;

namespace Open24.Domain.Interfaces;

public interface IGetAllRepository<TEntity> where TEntity : Base
{
    Task<IEnumerable<TEntity>> GetAllAsync();
}
