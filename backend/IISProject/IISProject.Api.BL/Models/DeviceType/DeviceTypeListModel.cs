namespace IISProject.Api.BL.Models.DeviceType;

public record DeviceTypeListModel : IModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}