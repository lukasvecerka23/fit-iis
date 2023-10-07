using IISProject.Api.BL.Facades;
using IISProject.Api.BL.Models.Device;
using Microsoft.AspNetCore.Mvc;

namespace IISProject.Controllers;

[ApiController]
[Route("api/devices")]
public class DeviceController : ControllerBase
{
    private readonly ILogger<DeviceController> _logger;
    private readonly DeviceFacade _deviceFacade;

    public DeviceController(ILogger<DeviceController> logger, DeviceFacade deviceFacade)
    {
        _logger = logger;
        _deviceFacade = deviceFacade;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<DeviceListModel>> Get()
    {
        return await _deviceFacade.GetAllAsync();
    }
}