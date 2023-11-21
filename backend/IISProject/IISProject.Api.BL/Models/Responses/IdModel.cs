namespace IISProject.Api.BL.Models.Responses;

public record IdModel: IModel
{
    public required Guid Id { get; set; }
}