namespace IISProject.Api.BL.Models.UserInSystem;

public record UserInSystemListModel : IModel
{
    public Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required Guid SystemId { get; set; }
}