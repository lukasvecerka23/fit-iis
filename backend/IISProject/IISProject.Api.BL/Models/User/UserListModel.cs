namespace IISProject.Api.BL.Models.User;

public record UserListModel : IModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
}