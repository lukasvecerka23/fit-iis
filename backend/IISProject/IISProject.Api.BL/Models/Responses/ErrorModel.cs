namespace IISProject.Api.BL.Models.Responses;

public record ErrorModel
{
    public required string Error { get; set; }
}