namespace IISProject.Api.BL.Models.Role;

public record RoleListModel : IModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}