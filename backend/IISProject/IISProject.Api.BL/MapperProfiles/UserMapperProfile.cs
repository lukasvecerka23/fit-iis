using AutoMapper;
using IISProject.Api.BL.Extensions;
using IISProject.Api.BL.Models.Responses;
using IISProject.Api.DAL.Entities;
using IISProject.Api.BL.Models.User;

namespace IISProject.Api.BL.MapperProfiles;

public class UserMapperProfile: Profile
{
    public UserMapperProfile()
    {
        CreateMap<UserEntity, UserListModel>()
            .MapMember(dst => dst.RoleName, src => src.Role!.Name);

        CreateMap<UserEntity, UserDetailModel>()
            .MapMember(dst => dst.RoleName, src => src.Role!.Name);

        CreateMap<UserCreateUpdateModel, UserEntity>()
            .Ignore(dst => dst.Id)
            .Ignore(dst => dst.Devices)
            .Ignore(dst => dst.Role)
            .Ignore(dst => dst.Kpis)
            .Ignore(dst => dst.UserInSystems)
            .Ignore(dst => dst.Measurements)
            .Ignore(dst => dst.PasswordHash);
        
        CreateMap<UserEntity, IdModel>();

    }
}