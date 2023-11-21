namespace IISProject.Api.BL.Models.Parameter;

public record ParameterCreateUpdateModel
{
    public required string Name { get; set; }
    public double? LowerLimit { get; set; }
    public double? UpperLimit { get; set; }
    
    public required Guid DeviceTypeId { get; set; }
}