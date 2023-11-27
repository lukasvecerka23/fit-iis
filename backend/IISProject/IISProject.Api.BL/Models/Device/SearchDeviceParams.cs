using Microsoft.AspNetCore.Mvc;

namespace IISProject.Api.BL.Models.Device;

public class SearchDeviceParams
{
    [FromQuery(Name = "systemId")] public Guid SystemId { get; set; } = Guid.Empty;
    [FromQuery(Name = "q")] public string Query { get; set; } = "";

    [FromQuery(Name = "p")] public int PageIndex { get; set; } = 0;

    [FromQuery(Name = "size")] public int PageSize { get; set; } = 10;
}