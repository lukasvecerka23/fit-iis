using AutoMapper;
using IISProject.Api.BL.Models.UserInSystem;
using IISProject.Api.DAL.Entities;

namespace IISProject.Api.BL.MapperProfiles;

public class UserInSystemMapperProfile: Profile
{
    public UserInSystemMapperProfile()
    {
        CreateMap<UserInSystemEntity, UserInSystemListModel>();
        CreateMap<UserInSystemEntity, UserInSystemDetailModel>();
        CreateMap<UserInSystemDetailModel, UserInSystemEntity>();
    }
    
}