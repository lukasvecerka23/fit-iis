namespace IISProject.Api.DAL.Entities;

public record UserEntity : IEntity
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    //public required string Password { get; set; }

    public ICollection<RoleOfUserEntity> Roles { get; set; } = new List<RoleOfUserEntity>();
    public ICollection<UserInSystemEntity> UserInSystems { get; set; } = new List<UserInSystemEntity>();
    public ICollection<DeviceEntity> Devices { get; set; } = new List<DeviceEntity>();
    public ICollection<MeasurementEntity> Measurements { get; set; } = new List<MeasurementEntity>();
    public ICollection<KpiEntity> Kpis { get; set; } = new List<KpiEntity>();
}