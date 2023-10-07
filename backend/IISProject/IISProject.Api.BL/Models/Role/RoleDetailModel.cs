using IISProject.Api.BL.Models.RoleOfUser;

namespace IISProject.Api.BL.Models.Role;

public record RoleDetailModel: IModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    
    public ICollection<RoleOfUserListModel> RoleOfUsers { get; set; } = new List<RoleOfUserListModel>();
}