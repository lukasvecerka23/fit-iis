using IISProject.Api.BL.Enums;

namespace IISProject.Api.BL.Models.System;

public record SystemListModel : IModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    
    public required Guid CreatorId { get; set; }
    public string CreatorName { get; set; } = null!;
    public int UsersCount { get; set; }
    public int DevicesCount { get; set; }
    
    public SystemStatus Status { get; set; }
    
    public bool CanEdit { get; set; }
    
    public AssignStatus AssignStatus { get; set; }
}