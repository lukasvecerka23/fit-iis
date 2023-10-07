using AutoMapper;
using IISProject.Api.BL.Extensions;
using IISProject.Api.BL.Models.Role;
using IISProject.Api.DAL.Entities;

namespace IISProject.Api.BL.MapperProfiles;

public class RoleMapperProfile: Profile
{
    public RoleMapperProfile()
    {
        CreateMap<RoleEntity, RoleListModel>();
        
        CreateMap<RoleEntity, RoleDetailModel>()
            .MapMember(src => src.RoleOfUsers, dst => dst.RoleOfUsers);

        CreateMap<RoleDetailModel, RoleEntity>()
            .Ignore(src => src.RoleOfUsers);
    }
}