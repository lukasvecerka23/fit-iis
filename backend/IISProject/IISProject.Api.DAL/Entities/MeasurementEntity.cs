namespace IISProject.Api.DAL.Entities;

public record MeasurementEntity : IEntity
{
    public required Guid Id { get; set; }
    public required double Value { get; set; }
    public required DateTime TimeStamp { get; set; }

    public DeviceEntity? Device { get; init; }
    public UserEntity? Creator { get; init; }
    public ParameterEntity? Parameter { get; init; }
}