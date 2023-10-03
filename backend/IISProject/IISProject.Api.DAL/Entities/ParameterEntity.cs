namespace IISProject.Api.DAL.Entities;

public record ParameterEntity : IEntity
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public double? LowerLimit { get; set; }
    public double? UpperLimit { get; set; }
    
    public required Guid DeviceTypeId { get; set; }
    
    public DeviceTypeEntity? DeviceType { get; init; }
    public ICollection<MeasurementEntity> Measurements { get; set; } = new List<MeasurementEntity>();
    public ICollection<KpiEntity> Kpis { get; set; } = new List<KpiEntity>();
}