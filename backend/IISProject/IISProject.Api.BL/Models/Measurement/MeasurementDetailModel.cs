namespace IISProject.Api.BL.Models.Measurement;

public record MeasurementDetailModel : IModel
{
    public Guid Id { get; set; }
    public required double Value { get; set; }
    public required DateTime TimeStamp { get; set; }
    
    public required Guid DeviceId { get; set; }
    public required Guid ParameterId { get; set; }
}