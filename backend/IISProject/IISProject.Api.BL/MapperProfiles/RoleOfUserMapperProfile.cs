using AutoMapper;
using IISProject.Api.BL.Extensions;
using IISProject.Api.BL.Models.RoleOfUser;
using IISProject.Api.DAL.Entities;

namespace IISProject.Api.BL.MapperProfiles;

public class RoleOfUserMapperProfile: Profile
{
    public RoleOfUserMapperProfile()
    {
        CreateMap<RoleOfUserEntity, RoleOfUserListModel>();
        CreateMap<RoleOfUserEntity, RoleOfUserDetailModel>();
        CreateMap<RoleOfUserDetailModel, RoleOfUserEntity>()
            .Ignore(dst => dst.User)
            .Ignore(dst => dst.Role);
    }
}