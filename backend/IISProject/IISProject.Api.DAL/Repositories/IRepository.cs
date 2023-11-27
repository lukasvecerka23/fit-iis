using IISProject.Api.DAL.Entities;

namespace IISProject.Api.DAL.Repositories;

public interface IRepository<TEntity> where TEntity : class, IEntity
{
    IQueryable<TEntity> GetAll();
    ValueTask<bool> ExistsAsync(Guid id);
    bool Exists(Guid id);
    Task<TEntity> InsertAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task DeleteAsync(Guid id);
}