using AutoMapper;
using IISProject.Api.BL.Extensions;
using IISProject.Api.BL.Models.Device;
using IISProject.Api.DAL.Entities;

namespace IISProject.Api.BL.MapperProfiles;

public class DeviceMapperProfile: Profile
{
    public DeviceMapperProfile()
    {
        CreateMap<DeviceEntity, DeviceListModel>();
        CreateMap<DeviceEntity, DeviceDetailModel>();
        CreateMap<DeviceDetailModel, DeviceEntity>()
            .Ignore(dst => dst.CreatorId)
            .Ignore(dst => dst.Creator)
            .Ignore(dst => dst.System)
            .Ignore(dst => dst.DeviceType)
            .Ignore(dst => dst.Measurements)
            .Ignore(dst => dst.Kpis);
    }
}