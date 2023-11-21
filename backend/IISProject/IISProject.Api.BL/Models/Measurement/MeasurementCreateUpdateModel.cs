namespace IISProject.Api.BL.Models.Measurement;

public record MeasurementCreateUpdateModel
{
    public required double Value { get; set; }
    public required DateTime TimeStamp { get; set; }
    
    public required Guid DeviceId { get; set; }
    public required Guid ParameterId { get; set; }
    public required Guid CreatorId { get; set; }
}