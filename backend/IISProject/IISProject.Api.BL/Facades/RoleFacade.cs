using AutoMapper;
using IISProject.Api.BL.Facades.Interfaces;
using IISProject.Api.BL.Models.Role;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.UnitOfWork;

namespace IISProject.Api.BL.Facades;

public class RoleFacade: FacadeBase<RoleEntity, RoleListModel, RoleDetailModel, RoleCreateUpdateModel>, IRoleFacade
{
    public RoleFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
        
    }
    
    public override List<string> NavigationPathDetails => new()
    {
        $"{nameof(RoleEntity.Users)}"
    };
}