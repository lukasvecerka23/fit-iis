using AutoMapper;
using IISProject.Api.BL.Extensions;
using IISProject.Api.BL.Models.Responses;
using IISProject.Api.BL.Models.UserInSystem;
using IISProject.Api.DAL.Entities;

namespace IISProject.Api.BL.MapperProfiles;

public class UserInSystemMapperProfile: Profile
{
    public UserInSystemMapperProfile()
    {
        CreateMap<UserInSystemEntity, UserInSystemListModel>()
            .MapMember(dst => dst.UserFullname, src => $"{src.User!.Name} {src.User.Surname}");
        
        CreateMap<UserInSystemEntity, UserInSystemDetailModel>();
        
        CreateMap<UserInSystemCreateUpdateModel, UserInSystemEntity>()
            .Ignore(dst => dst.Id)
            .Ignore(dst => dst.User)
            .Ignore(dst => dst.System);
        
        CreateMap<UserInSystemEntity, IdModel>();
    }
    
}