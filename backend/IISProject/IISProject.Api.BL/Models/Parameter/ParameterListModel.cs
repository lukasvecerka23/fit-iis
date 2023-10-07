namespace IISProject.Api.BL.Models.Parameter;

public record ParameterListModel : IModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public double? LowerLimit { get; set; }
    public double? UpperLimit { get; set; }
    
    public required Guid DeviceTypeId { get; set; }
}