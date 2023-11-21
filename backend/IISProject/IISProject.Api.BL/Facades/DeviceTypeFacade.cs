using AutoMapper;
using IISProject.Api.BL.Facades.Interfaces;
using IISProject.Api.BL.Models.DeviceType;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.UnitOfWork;

namespace IISProject.Api.BL.Facades;

public class DeviceTypeFacade: FacadeBase<DeviceTypeEntity, DeviceTypeListModel, DeviceTypeDetailModel, DeviceTypeCreateUpdateModel>, IDeviceTypeFacade
{
    public DeviceTypeFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
        
    }
    
    public override List<string> NavigationPathDetails => new()
    {
        $"{nameof(DeviceTypeEntity.Devices)}",
        $"{nameof(DeviceTypeEntity.Parameters)}"
    };
}