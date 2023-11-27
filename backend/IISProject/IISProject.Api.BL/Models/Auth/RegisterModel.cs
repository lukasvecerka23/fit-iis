namespace IISProject.Api.BL.Models.Auth;

public record RegisterModel
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}