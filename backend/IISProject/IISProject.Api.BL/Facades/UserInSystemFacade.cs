using AutoMapper;
using IISProject.Api.BL.Facades.Interfaces;
using IISProject.Api.BL.Models.UserInSystem;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.UnitOfWork;

namespace IISProject.Api.BL.Facades;

public class UserInSystemFacade: FacadeBase<UserInSystemEntity, UserInSystemListModel, UserInSystemDetailModel, UserInSystemCreateUpdateModel>, IUserInSystemFacade
{
    public UserInSystemFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
        
    }
}