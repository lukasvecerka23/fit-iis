namespace IISProject.Api.BL.Models.User;

public record UserCreateUpdateModel
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
}