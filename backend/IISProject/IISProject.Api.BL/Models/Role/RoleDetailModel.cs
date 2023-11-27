using IISProject.Api.BL.Models.User;

namespace IISProject.Api.BL.Models.Role;

public record RoleDetailModel: IModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public List<UserListModel> Users { get; set; } = new();
}