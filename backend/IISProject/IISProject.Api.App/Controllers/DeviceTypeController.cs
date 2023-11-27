using IISProject.Api.BL.Facades;
using IISProject.Api.BL.Models.DeviceType;
using IISProject.Api.BL.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace IISProject.Controllers;

[ApiController]
[Route("api/deviceTypes")]
public class DeviceTypeController : ControllerBase
{
    private readonly ILogger<DeviceTypeController> _logger;
    private readonly DeviceTypeFacade _deviceTypeFacade;

    public DeviceTypeController(ILogger<DeviceTypeController> logger, DeviceTypeFacade deviceTypeFacade)
    {
        _logger = logger;
        _deviceTypeFacade = deviceTypeFacade;
    }

    [HttpGet]
    public async Task<IEnumerable<DeviceTypeListModel>> GetDeviceTypes()
    {
        return await _deviceTypeFacade.GetAllAsync();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<DeviceTypeDetailModel>> GetDeviceTypeById(Guid id)
    {
        var result = await _deviceTypeFacade.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound(new ErrorModel {Error = $"Device type with id {id} not found"});
        }
        return result;
    }

    [HttpPost]
    public async Task<ActionResult<IdModel>> CreateDevice(DeviceTypeCreateUpdateModel deviceType)
    {
        var result = await _deviceTypeFacade.CreateAsync(deviceType);
        return Created($"/api/deviceTypes/{result.Id}", result);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<IdModel>> UpdateDeviceType(Guid id, DeviceTypeCreateUpdateModel deviceType)
    {
        var result = await _deviceTypeFacade.UpdateAsync(deviceType, id);
        if (result == null)
        {
            return NotFound(new ErrorModel {Error = $"Device type with id {id} not found"});
        }
        
        return result;
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteDeviceType(Guid id)
    {
        var result = await _deviceTypeFacade.DeleteAsync(id);
        if (!result)
        {
            return NotFound(new ErrorModel {Error = $"Device type with id {id} not found"});
        }

        return Ok();
    }
    
    [HttpGet("search")]
    public async Task<ActionResult<DeviceTypeSearchModel>> Search([FromQuery] SearchDeviceTypeParams searchParams)
    {
        var result = await _deviceTypeFacade.SearchAsync(searchParams.Query, searchParams.PageIndex, searchParams.PageSize);
        return result;
    }
}

public class SearchDeviceTypeParams
{
    [FromQuery(Name = "q")] public string Query { get; set; } = "";

    [FromQuery(Name = "p")] public int PageIndex { get; set; } = 0;

    [FromQuery(Name = "size")] public int PageSize { get; set; } = 10;
}