using AutoMapper;
using IISProject.Api.BL.Facades.Interfaces;
using IISProject.Api.BL.Models.User;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.UnitOfWork;

namespace IISProject.Api.BL.Facades;

public class UserFacade: FacadeBase<UserEntity, UserListModel, UserDetailModel>, IUserFacade
{
    public UserFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
        
    }
    
    public override List<string> NavigationPathDetails => new()
    {
        $"{nameof(UserEntity.Roles)}",
        $"{nameof(UserEntity.UserInSystems)}",
        $"{nameof(UserEntity.Kpis)}",
        $"{nameof(UserEntity.Devices)}"
    };
}