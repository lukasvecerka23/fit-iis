namespace IISProject.Api.BL.Models.RoleOfUser;

public record RoleOfUserListModel : IModel
{
    public Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required Guid RoleId { get; set; }
    public required string RoleName { get; set; }
}