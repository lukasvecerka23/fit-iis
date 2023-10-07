using AutoMapper;
using IISProject.Api.BL.Facades.Interfaces;
using IISProject.Api.BL.Models.RoleOfUser;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.UnitOfWork;

namespace IISProject.Api.BL.Facades;

public class RoleOfUserFacade: FacadeBase<RoleOfUserEntity, RoleOfUserListModel, RoleOfUserDetailModel>, IRoleOfUserFacade
{
    public RoleOfUserFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
        
    }
}