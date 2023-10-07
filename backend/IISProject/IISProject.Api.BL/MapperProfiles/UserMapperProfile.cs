using AutoMapper;
using IISProject.Api.BL.Extensions;
using IISProject.Api.DAL.Entities;
using IISProject.Api.BL.Models.User;

namespace IISProject.Api.BL.MapperProfiles;

public class UserMapperProfile: Profile
{
    public UserMapperProfile()
    {
        CreateMap<UserEntity, UserListModel>();
        
        CreateMap<UserEntity, UserDetailModel>()
            .MapMember(dst => dst.Devices, src => src.Devices)
            .MapMember(dst => dst.RoleOfUsers, src => src.Roles)
            .MapMember(dst => dst.Kpis, src => src.Kpis)
            .MapMember(dst => dst.Systems, src => src.UserInSystems);

        CreateMap<UserDetailModel, UserEntity>()
            .Ignore(dst => dst.Devices)
            .Ignore(dst => dst.Roles)
            .Ignore(dst => dst.Kpis)
            .Ignore(dst => dst.UserInSystems)
            .Ignore(dst => dst.Measurements);

    }
}