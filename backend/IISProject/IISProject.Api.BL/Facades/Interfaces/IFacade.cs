using IISProject.Api.BL.Models;
using IISProject.Api.BL.Models.Responses;
using IISProject.Api.DAL.Entities;

namespace IISProject.Api.BL.Facades.Interfaces;

public interface IFacade<TEntity, TListModel, TDetailModel, TCreateUpdateModel>
    where TEntity : class, IEntity
    where TListModel : IModel
    where TDetailModel : class, IModel
    where TCreateUpdateModel : class
{
    void IncludeNavigationPathDetails(ref IQueryable<TEntity> query);
    
    Task<IEnumerable<TListModel>> GetAllAsync();
    Task<TDetailModel?> GetByIdAsync(Guid id);
    Task<IdModel> CreateAsync(TCreateUpdateModel model);
    Task<IdModel?> UpdateAsync(TCreateUpdateModel model, Guid id);
    Task<bool> DeleteAsync(Guid id);
}