using Microsoft.AspNetCore.Mvc;

namespace IISProject.Api.BL.Models.AssignToSystem;

public class AssignToSystemParams
{
    [FromQuery(Name = "systemId")] public Guid SystemId { get; set; } = Guid.Empty;
}