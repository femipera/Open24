using Open24.Domain.Entities;

namespace Open24.Domain.Interfaces;

public interface IGetByIdRepository<TEntity> where TEntity : Base
{
    Task<TEntity> GetByIdAsync(int id);
}