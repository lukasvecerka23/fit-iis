namespace IISProject.Api.BL.Models.System;

public record SystemCreateUpdateModel
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required Guid CreatorId { get; set; } 
    
    public required List<Guid> DeviceIds { get; set; }
}