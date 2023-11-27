namespace IISProject.Api.BL.Models.Kpi;

public record KpiDetailModel : IModel
{
    public Guid Id { get; set; }
    public required string Function { get; set; }
    public bool? Error { get; set; }
    public required Guid DeviceId { get; set; }
    public required Guid ParameterId { get; set; }
    public required Guid CreatorId { get; set; }
}