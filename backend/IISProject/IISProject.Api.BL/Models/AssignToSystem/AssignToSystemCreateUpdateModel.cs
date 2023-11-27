namespace IISProject.Api.BL.Models.AssignToSystem;

public class AssignToSystemCreateUpdateModel
{
    public required Guid UserId { get; set; }
    public required Guid SystemId { get; set; }
}