using IISProject.Api.BL.Facades;
using IISProject.Api.BL.Models.Device;
using IISProject.Api.BL.Models.Responses;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpGet]
    public async Task<IEnumerable<DeviceListModel>> GetDevices()
    {
        return await _deviceFacade.GetAllAsync();
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<DeviceDetailModel>> GetDeviceById(Guid id)
    {
        var result = await _deviceFacade.GetByIdAsync(id);
        
        if (result == null)
        {
            return NotFound(new ErrorModel {Error = $"Device with id {id} not found"});
        }

        return result;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<IdModel>> CreateDevice(DeviceCreateUpdateModel device)
    {
        var result = await _deviceFacade.CreateWithCheckAsync(device);
        if (result == null)
        {
            return NotFound(new ErrorModel {Error = $"The identifier already exists"});
        }
        return Created($"/api/devices/{result.Id}", result);
    }
    
    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<IdModel>> UpdateDevice(Guid id, DeviceCreateUpdateModel device)
    {

        var result = await _deviceFacade.UpdateAsync(device, id);

        if (result == null)
        {
            return NotFound(new ErrorModel {Error = $"Device with id {id} not found"});
        }
        
        return result;
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteDevice(Guid id)
    {
        var result = await _deviceFacade.DeleteAsync(id);
        
        if (!result)
        {
            return NotFound(new ErrorModel {Error = $"Device with id {id} not found"});
        }

        return Ok();
    }

    [Authorize]
    [HttpGet("search")]
    public async Task<ActionResult<DeviceSearchModel>> Search([FromQuery] SearchDeviceParams searchParams)
    {
        var result = await _deviceFacade.SearchAsync(searchParams);
        return result;
    }
}