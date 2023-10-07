using AutoMapper;
using IISProject.Api.BL.Extensions;
using IISProject.Api.BL.Models.System;
using IISProject.Api.DAL.Entities;

namespace IISProject.Api.BL.MapperProfiles;

public class SystemMapperProfile: Profile
{
    public SystemMapperProfile()
    {
        CreateMap<SystemEntity, SystemListModel>();
        CreateMap<SystemEntity, SystemDetailModel>()
            .MapMember(dst => dst.Devices, src => src.Devices)
            .MapMember(dst => dst.Users, src => src.UsersInSystem);

        CreateMap<SystemDetailModel, SystemEntity>()
            .Ignore(dst => dst.Creator)
            .Ignore(dst => dst.Devices)
            .Ignore(dst => dst.UsersInSystem);
    }
}