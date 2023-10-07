using IISProject.Api.BL.Models;
using IISProject.Api.DAL.Entities;

namespace IISProject.Api.BL.Facades.Interfaces;

public interface IFacade<TEntity, TListModel, TDetailModel>
    where TEntity : class, IEntity
    where TListModel : IModel
    where TDetailModel : class, IModel
{
    void IncludeNavigationPathDetails(ref IQueryable<TEntity> query);
    
    Task<IEnumerable<TListModel>> GetAllAsync();
    Task<TDetailModel?> GetByIdAsync(Guid id);
    Task<TDetailModel> SaveAsync(TDetailModel model);
    Task DeleteAsync(Guid id);
}