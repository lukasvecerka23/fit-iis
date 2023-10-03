namespace IISProject.Api.DAL.Entities;

public record KpiEntity : IEntity
{
    public required Guid Id { get; set; }
    public required string Function { get; set; }
    public bool? Error { get; set; }

    public UserEntity? Creator { get; init; }
    public DeviceEntity? Device { get; init; }
    public ParameterEntity? Parameter { get; init; }
    
}