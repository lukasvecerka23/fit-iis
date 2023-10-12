using AutoMapper;
using IISProject.Api.BL.Facades.Interfaces;
using IISProject.Api.BL.Models.System;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.UnitOfWork;

namespace IISProject.Api.BL.Facades;

public class SystemFacade: FacadeBase<SystemEntity, SystemListModel, SystemDetailModel>, ISystemFacade
{
    public SystemFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
        
    }
    
    public override List<string> NavigationPathDetails => new()
    {
        $"{nameof(SystemEntity.Devices)}",
        $"{nameof(SystemEntity.UsersInSystem)}"
    };
}