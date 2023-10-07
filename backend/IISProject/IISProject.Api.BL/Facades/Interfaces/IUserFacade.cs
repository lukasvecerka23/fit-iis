using IISProject.Api.BL.Models.User;
using IISProject.Api.DAL.Entities;

namespace IISProject.Api.BL.Facades.Interfaces;

public interface IUserFacade: IFacade<UserEntity, UserListModel, UserDetailModel>
{
    
}