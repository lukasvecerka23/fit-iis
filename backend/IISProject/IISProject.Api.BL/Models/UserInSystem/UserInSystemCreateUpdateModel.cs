namespace IISProject.Api.BL.Models.UserInSystem;

public class UserInSystemCreateUpdateModel
{
    public required Guid UserId { get; set; }
    public required Guid SystemId { get; set; }
}