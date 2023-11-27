using AutoMapper;
using IISProject.Api.BL.Extensions;
using IISProject.Api.BL.Models.Responses;
using IISProject.Api.BL.Models.Role;
using IISProject.Api.DAL.Entities;

namespace IISProject.Api.BL.MapperProfiles;

public class RoleMapperProfile: Profile
{
    public RoleMapperProfile()
    {
        CreateMap<RoleEntity, RoleListModel>();
        
        CreateMap<RoleEntity, RoleDetailModel>()
            .MapMember(dst => dst.Users, src => src.Users);

        CreateMap<RoleCreateUpdateModel, RoleEntity>()
            .Ignore(dst => dst.Id)
            .Ignore(dst => dst.Users);
        
        CreateMap<RoleEntity, IdModel>();
    }
}