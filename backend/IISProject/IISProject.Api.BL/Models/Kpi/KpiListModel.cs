using IISProject.Api.Common.Enum;

namespace IISProject.Api.BL.Models.Kpi;

public record KpiListModel : IModel
{
    public Guid Id { get; set; }
    public bool? Error { get; set; }
    public required Guid DeviceId { get; set; }
    public required Guid CreatorId { get; set; }
    public required KpiFunction Function { get; set; }
}