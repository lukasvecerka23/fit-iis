using IISProject.Api.BL.Models.Kpi;

namespace IISProject.Api.BL.Models.Parameter;

public record ParameterDetailModel : IModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public double? LowerLimit { get; set; }
    public double? UpperLimit { get; set; }
    
    public required Guid DeviceTypeId { get; set; }
    
    public ICollection<KpiListModel> Kpis { get; set; } = new List<KpiListModel>();
}