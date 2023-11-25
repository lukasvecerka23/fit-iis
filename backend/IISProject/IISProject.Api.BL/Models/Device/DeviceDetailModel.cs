using IISProject.Api.BL.Models.Parameter;

namespace IISProject.Api.BL.Models.Device;

public record DeviceDetailModel : IModel
{
    public Guid Id { get; set; }
    public required string UserAlias { get; set; }
    public string? Description { get; set; }
    public Guid? SystemId { get; set; }
    public required Guid DeviceTypeId { get; set; }
    public required string DeviceTypeName { get; set; }
    public string? SystemName { get; set; }
    public string? CreatorName { get; set; }
    public ICollection<ParameterListModel> Parameters { get; set; } = new List<ParameterListModel>();
}