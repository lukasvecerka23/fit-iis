namespace IISProject.Api.BL.Models.AssignToSystem;

public class AssignToSystemDetailModel: IModel
{
    public Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required Guid SystemId { get; set; }
    public required string UserFullName { get; set; }
    public required string UserName { get; set; }
}