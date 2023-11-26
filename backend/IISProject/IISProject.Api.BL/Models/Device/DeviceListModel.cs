namespace IISProject.Api.BL.Models.Device;

public record DeviceListModel : IModel
{
    public Guid Id { get; set; }
    public required string UserAlias { get; set; }
    public Guid? SystemId { get; set; }
    public required Guid DeviceTypeId { get; set; }
    public required string DeviceTypeName { get; set; }
    public string? SystemName { get; set; }
    public string? CreatorName { get; set; }
    public Guid CreatorId { get; set; }
}