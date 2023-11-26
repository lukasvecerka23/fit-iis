using IISProject.Api.BL.Enums;

namespace IISProject.Api.BL.Models.Parameter;

public record ParameterStatusListModel : IModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public double? LowerLimit { get; set; }
    public double? UpperLimit { get; set; }
    public ParameterStatus Status { get; set; }
    
    public required Guid DeviceTypeId { get; set; }
}