using AutoMapper;

namespace IISProject.Api.DAL.Entities;

public record RoleEntity : IEntity
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }

    public ICollection<RoleOfUserEntity> RoleOfUsers { get; set; } = new List<RoleOfUserEntity>();
    
    public class RoleEntityProfile : Profile
    {
        public RoleEntityProfile()
        {
            CreateMap<RoleEntity, RoleEntity>();
        }
    }
}