using Microsoft.EntityFrameworkCore;
using Open24.Domain.Entities;
using Open24.Domain.Interfaces;
using Open24.Infra.Data.Context;

namespace Open24.Infra.Data.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Base, new()
{
    #region Properties
    protected readonly Open24DbContext _dbcontext;

    protected BaseRepository(Open24DbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }
    #endregion

    #region Create
    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        _dbcontext.Set<TEntity>().Add(entity);
        await _dbcontext.SaveChangesAsync();

        return entity;
    }
    #endregion

    #region GetById
    public virtual async Task<TEntity> GetByIdAsync(Guid id)
    {
        return await _dbcontext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
    }
    #endregion

    #region GetAll
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(int count = -1, int skip = -1, string search = "")
    {
        var entities = new List<TEntity>();

        if (count != -1 && skip != -1)
            entities = await _dbcontext.Set<TEntity>()
                .OrderByDescending(p => p.Id)
                .Skip(skip)
                .Take(count)
                .ToListAsync();
        else
            entities = await _dbcontext.Set<TEntity>()
                .OrderByDescending(p => p.Id)
                .ToListAsync();

        return entities;
    }
    #endregion

    #region Update
    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _dbcontext.Set<TEntity>().Update(entity);
        await _dbcontext.SaveChangesAsync();
        return entity;
    }
    #endregion

    #region Delete
    public virtual async Task DeleteAsync(TEntity entity)
    {
        _dbcontext.Set<TEntity>().Remove(entity);
        await _dbcontext.SaveChangesAsync();
    }
    #endregion

    #region Dispose
    public void Dispose()
    {
        _dbcontext?.Dispose();
    }
    #endregion
}
