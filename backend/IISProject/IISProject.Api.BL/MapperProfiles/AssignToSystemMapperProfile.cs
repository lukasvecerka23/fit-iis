using AutoMapper;
using IISProject.Api.BL.Extensions;
using IISProject.Api.BL.Models.AssignToSystem;
using IISProject.Api.BL.Models.Responses;
using IISProject.Api.DAL.Entities;

namespace IISProject.Api.BL.MapperProfiles;

public class AssignToSystemMapperProfile: Profile
{
    public AssignToSystemMapperProfile()
    {
        CreateMap<AssignToSystemEntity, AssignToSystemListModel>()
            .MapMember(dst => dst.UserFullName, src => $"{src.User!.Name} {src.User.Surname}")
            .MapMember(dst => dst.UserName, src => src.User!.Username);
        
        CreateMap<AssignToSystemEntity, AssignToSystemDetailModel>()
            .MapMember(dst => dst.UserFullName, src => $"{src.User!.Name} {src.User.Surname}")
            .MapMember(dst => dst.UserName, src => src.User!.Username);

        CreateMap<AssignToSystemCreateUpdateModel, AssignToSystemEntity>()
            .Ignore(dst => dst.Id)
            .Ignore(dst => dst.User)
            .Ignore(dst => dst.System);
        
        CreateMap<AssignToSystemEntity, IdModel>();
    }
}