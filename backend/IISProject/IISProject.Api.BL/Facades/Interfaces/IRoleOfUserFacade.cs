using IISProject.Api.BL.Models.RoleOfUser;
using IISProject.Api.DAL.Entities;

namespace IISProject.Api.BL.Facades.Interfaces;

public interface IRoleOfUserFacade: IFacade<RoleOfUserEntity, RoleOfUserListModel, RoleOfUserDetailModel, RoleOfUserCreateUpdateModel>
{
    
}