using Open24.Domain.Entities;
using System.Linq.Expressions;

namespace Open24.Domain.Interfaces;

public interface IBaseRepository<TEntity> : IDisposable where TEntity : Base
{
    Task<TEntity> CreateAsync(TEntity entity);

    Task<TEntity> UpdateAsync(TEntity entity);

    Task<TEntity> GetByIdAsync(Guid id);

    Task<IEnumerable<TEntity>> GetAllAsync(int count = -1, int skip = -1, string search = "");

    Task DeleteAsync(TEntity entity);
}
