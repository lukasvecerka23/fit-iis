using AutoMapper;
using IISProject.Api.BL.Facades.Interfaces;
using IISProject.Api.BL.Models.Role;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.UnitOfWork;

namespace IISProject.Api.BL.Facades;

public class RoleFacade: FacadeBase<RoleEntity, RoleListModel, RoleDetailModel>, IRoleFacade
{
    public RoleFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
        
    }
}