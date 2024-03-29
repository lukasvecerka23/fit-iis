using IISProject.Api.Common.Enum;

namespace IISProject.Api.BL.Models.Kpi;

public record KpiListModel : IModel
{
    public Guid Id { get; set; }
    public bool? Error { get; set; }
    public required Guid DeviceId { get; set; }
    public required Guid CreatorId { get; set; }
    public required Guid ParameterId { get; set; }
    public required KpiFunction Function { get; set; }
    public double? LastMeasurement { get; set; }
    public required string ParameterName { get; set; }
    public required int Value { get; set; }
}