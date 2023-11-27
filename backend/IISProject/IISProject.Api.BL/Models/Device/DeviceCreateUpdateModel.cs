namespace IISProject.Api.BL.Models.Device;

public record DeviceCreateUpdateModel
{    
    public required string UserId { get; set; }
    public string? UserAlias { get; set; }
    public string? Description { get; set; }
    public Guid? SystemId { get; set; }
    public required Guid DeviceTypeId { get; set; }
    public required Guid CreatorId { get; set; }
}