using IISProject.Api.BL.Models.DeviceType;
using IISProject.Api.DAL.Entities;

namespace IISProject.Api.BL.Facades.Interfaces;

public interface IDeviceTypeFacade: IFacade<DeviceTypeEntity, DeviceTypeListModel, DeviceTypeDetailModel>
{
    
}