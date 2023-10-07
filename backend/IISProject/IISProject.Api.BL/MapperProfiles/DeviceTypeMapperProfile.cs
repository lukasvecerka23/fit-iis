using AutoMapper;
using AutoMapper.Configuration.Annotations;
using IISProject.Api.BL.Extensions;
using IISProject.Api.BL.Models.DeviceType;
using IISProject.Api.DAL.Entities;

namespace IISProject.Api.BL.MapperProfiles;

public class DeviceTypeMapperProfile: Profile
{
    public DeviceTypeMapperProfile()
    {
        CreateMap<DeviceTypeEntity, DeviceTypeListModel>();
        
        CreateMap<DeviceTypeEntity, DeviceTypeDetailModel>()
            .MapMember(dst => dst.Parameters, src => src.Parameters)
            .MapMember(dst => dst.Devices, src => src.Devices);
        
        CreateMap<DeviceTypeDetailModel, DeviceTypeEntity>()
            .Ignore(dst => dst.Parameters)
            .Ignore(dst => dst.Devices);
    }
    
}