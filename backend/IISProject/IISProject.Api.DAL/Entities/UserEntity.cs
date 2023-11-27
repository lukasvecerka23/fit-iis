using AutoMapper;

namespace IISProject.Api.DAL.Entities;

public record UserEntity : IEntity
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }

    public required Guid RoleId { get; set; }
    
    public RoleEntity? Role { get; set; }
    
    public ICollection<UserInSystemEntity> UserInSystems { get; set; } = new List<UserInSystemEntity>();
    public ICollection<DeviceEntity> Devices { get; set; } = new List<DeviceEntity>();
    public ICollection<KpiEntity> Kpis { get; set; } = new List<KpiEntity>();
    
    public ICollection<AssignToSystemEntity> AssignsToSystems { get; set; } = new List<AssignToSystemEntity>();
    
    public class UserEntityProfile : Profile
    {
        public UserEntityProfile()
        {
            CreateMap<UserEntity, UserEntity>();
        }
    }
}