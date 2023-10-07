namespace IISProject.Api.BL.Models.System;

public record SystemListModel : IModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    
    public required Guid CreatorId { get; set; }
}