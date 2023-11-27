using AutoMapper;

namespace IISProject.Api.DAL.Entities;

public record AssignToSystemEntity : IEntity
{
    public Guid Id { get; set; }

    public required Guid UserId { get; set; }
    public required Guid SystemId { get; set; }

    public UserEntity? User { get; init; }
    public SystemEntity? System { get; init; }
    
    public class AssignToSystemEntityProfile : Profile
    {
        public AssignToSystemEntityProfile()
        {
            CreateMap<AssignToSystemEntity, AssignToSystemEntity>();
        }
    }
}