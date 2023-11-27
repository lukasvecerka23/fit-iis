using IISProject.Api.BL.Models.Role;
using IISProject.Api.DAL.Entities;

namespace IISProject.Api.BL.Facades.Interfaces;

public interface IRoleFacade: IFacade<RoleEntity, RoleListModel, RoleDetailModel, RoleCreateUpdateModel>
{
    
}