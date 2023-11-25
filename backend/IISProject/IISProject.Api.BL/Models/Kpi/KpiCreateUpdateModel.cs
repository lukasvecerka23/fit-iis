using IISProject.Api.Common.Enum;

namespace IISProject.Api.BL.Models.Kpi;

public record KpiCreateUpdateModel
{
    public required KpiFunction Function { get; set; }
    public bool? Error { get; set; }
    public required Guid DeviceId { get; set; }
    public required Guid ParameterId { get; set; }
    public required Guid CreatorId { get; set; }
    public required int Value { get; set; }
}