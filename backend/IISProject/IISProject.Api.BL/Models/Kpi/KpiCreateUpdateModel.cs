namespace IISProject.Api.BL.Models.Kpi;

public record KpiCreateUpdateModel
{
    public required string Function { get; set; }
    public bool? Error { get; set; }
    public required Guid DeviceId { get; set; }
    public required Guid ParameterId { get; set; }
    public required Guid CreatorId { get; set; }
}