namespace IISProject.Api.BL.Models.Device;

public record DeviceDetailModel : IModel
{
    public Guid Id { get; set; }
    public required string UserAlias { get; set; }
    public string? Description { get; set; }
    public Guid? SystemId { get; set; }
    public required Guid DeviceTypeId { get; set; }
}