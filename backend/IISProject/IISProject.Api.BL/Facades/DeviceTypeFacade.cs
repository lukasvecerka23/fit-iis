using AutoMapper;
using IISProject.Api.BL.Facades.Interfaces;
using IISProject.Api.BL.Models.DeviceType;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.UnitOfWork;

namespace IISProject.Api.BL.Facades;

public class DeviceTypeFacade: FacadeBase<DeviceTypeEntity, DeviceTypeListModel, DeviceTypeDetailModel>, IDeviceTypeFacade
{
    public DeviceTypeFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
        
    }
}