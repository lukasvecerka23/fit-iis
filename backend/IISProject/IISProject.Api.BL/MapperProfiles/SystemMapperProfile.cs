using AutoMapper;
using IISProject.Api.BL.Extensions;
using IISProject.Api.BL.Models.Responses;
using IISProject.Api.BL.Models.System;
using IISProject.Api.DAL.Entities;

namespace IISProject.Api.BL.MapperProfiles;

public class SystemMapperProfile: Profile
{
    public SystemMapperProfile()
    {
        CreateMap<SystemEntity, SystemListModel>()
            .MapMember(dst => dst.CreatorName, src => $"{src.Creator!.Name} {src.Creator.Surname}")
            .MapMember(dst => dst.UsersCount, src => src.UsersInSystem.Count)
            .MapMember(dst => dst.DevicesCount, src => src.Devices.Count)
            .Ignore(dst => dst.Status);
        
        CreateMap<SystemEntity, SystemDetailModel>()
            .MapMember(dst => dst.Devices, src => src.Devices)
            .MapMember(dst => dst.Users, src => src.UsersInSystem)
            .MapMember(dst => dst.CreatorName, src => $"{src.Creator!.Name} {src.Creator.Surname}");

        CreateMap<SystemCreateUpdateModel, SystemEntity>()
            .Ignore(dst => dst.Id)
            .Ignore(dst => dst.Creator)
            .Ignore(dst => dst.Devices)
            .Ignore(dst => dst.UsersInSystem);
        
        CreateMap<SystemEntity, IdModel>();
    }
}