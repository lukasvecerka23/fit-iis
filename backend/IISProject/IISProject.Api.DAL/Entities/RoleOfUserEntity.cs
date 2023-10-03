namespace IISProject.Api.DAL.Entities;

public record RoleOfUserEntity : IEntity
{
    public Guid Id { get; set; }

    public required Guid UserId { get; set; }
    public required Guid RoleId { get; set; }
    
    public UserEntity? User { get; init; }
    public RoleEntity? Role { get; init; }
}