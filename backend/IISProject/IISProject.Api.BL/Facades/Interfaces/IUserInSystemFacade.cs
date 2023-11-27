using IISProject.Api.BL.Models.UserInSystem;
using IISProject.Api.DAL.Entities;

namespace IISProject.Api.BL.Facades.Interfaces;

public interface IUserInSystemFacade: IFacade<UserInSystemEntity, UserInSystemListModel, UserInSystemDetailModel, UserInSystemCreateUpdateModel>
{
    
}