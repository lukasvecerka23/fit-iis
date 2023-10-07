using AutoMapper;
using IISProject.Api.BL.Models.Device;
using IISProject.Api.DAL.Entities;

namespace IISProject.Api.BL.MapperProfiles;

public class DeviceMapperProfile: Profile
{
    public DeviceMapperProfile()
    {
        CreateMap<DeviceEntity, DeviceListModel>();
        CreateMap<DeviceEntity, DeviceDetailModel>();
        CreateMap<DeviceDetailModel, DeviceEntity>();
    }
}