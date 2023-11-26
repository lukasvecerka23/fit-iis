using AutoMapper;
using IISProject.Api.BL.Extensions;
using IISProject.Api.BL.Models.Device;
using IISProject.Api.BL.Models.Responses;
using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace IISProject.Api.BL.MapperProfiles;

public class DeviceMapperProfile: Profile
{
    public DeviceMapperProfile()
    {
        CreateMap<DeviceEntity, DeviceListModel>()
            .MapMember(dst => dst.DeviceTypeName, src => src.DeviceType!.Name)
            .MapMember(dst => dst.CreatorName!, src => $"{src.Creator!.Name} {src.Creator.Surname}")
            .MapMember(dst =>dst.SystemName!, src => src.System!.Name)
            .MapMember(dst =>dst.SystemName!, src => src.System!.Name)
            .MapMember(dst => dst.CreatorId, src => src.CreatorId);
        
        CreateMap<DeviceEntity, DeviceStatusListModel>()
            .MapMember(dst => dst.DeviceTypeName, src => src.DeviceType!.Name)
            .MapMember(dst => dst.CreatorName!, src => $"{src.Creator!.Name} {src.Creator.Surname}")
            .MapMember(dst =>dst.SystemName!, src => src.System!.Name)
            .Ignore(dst => dst.Status);
            

        CreateMap<DeviceEntity, DeviceDetailModel>()
            .MapMember(dst => dst.DeviceTypeName, src => src.DeviceType!.Name)
            .MapMember(dst => dst.CreatorName!, src => $"{src.Creator!.Name} {src.Creator.Surname}")
            .MapMember(dst => dst.SystemName!, src => src.System!.Name)
            .MapMember(dst => dst.Parameters, src => src.DeviceType!.Parameters)
            .MapMember(dst => dst.CreatorId, src => src.CreatorId);
        CreateMap<DeviceEntity, DeviceCreateUpdateModel>();
        
        CreateMap<DeviceCreateUpdateModel, DeviceEntity>()
            .Ignore(dst => dst.Id)
            .Ignore(dst => dst.Creator)
            .Ignore(dst => dst.System)
            .Ignore(dst => dst.DeviceType)
            .Ignore(dst => dst.Measurements)
            .Ignore(dst => dst.Kpis);

        CreateMap<DeviceEntity, IdModel>();
    }
}