using IISProject.Api.BL.Models.Device;
using IISProject.Api.BL.Models.Parameter;

namespace IISProject.Api.BL.Models.DeviceType;

public record DeviceTypeDetailModel : IModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    
    public ICollection<ParameterListModel> Parameters { get; set; } = new List<ParameterListModel>();
    public ICollection<DeviceListModel> Devices { get; set; } = new List<DeviceListModel>();
    
}