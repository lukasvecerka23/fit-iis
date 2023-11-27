using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.Repositories;

namespace IISProject.Api.DAL.UnitOfWork;

public interface IUnitOfWork: IAsyncDisposable
{
    IRepository<TEntity> GetRepository<TEntity>()
        where TEntity : class, IEntity;

    Task CommitAsync();
}