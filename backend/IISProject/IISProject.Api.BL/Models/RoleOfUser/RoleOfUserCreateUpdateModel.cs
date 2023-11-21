namespace IISProject.Api.BL.Models.RoleOfUser;

public class RoleOfUserCreateUpdateModel
{
    public required Guid UserId { get; set; }
    public required Guid RoleId { get; set; }
}