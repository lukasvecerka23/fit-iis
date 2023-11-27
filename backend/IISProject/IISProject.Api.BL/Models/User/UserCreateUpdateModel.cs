namespace IISProject.Api.BL.Models.User;

public record UserCreateUpdateModel
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required Guid RoleId { get; set; }
}