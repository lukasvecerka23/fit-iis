using AutoMapper;

namespace IISProject.Api.DAL.Entities;

public record SystemEntity : IEntity
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    
    public Guid? CreatorId { get; set; }
    
    public UserEntity? Creator { get; init; }
    public ICollection<UserInSystemEntity> UsersInSystem { get; set; } = new List<UserInSystemEntity>();
    public ICollection<DeviceEntity> Devices { get; set; } = new List<DeviceEntity>();
    
    public ICollection<AssignToSystemEntity> AssignsToSystems { get; set; } = new List<AssignToSystemEntity>();

    public class SystemEntityProfile : Profile
    {
        public SystemEntityProfile()
        {
            CreateMap<SystemEntity, SystemEntity>();
        }
    }
}