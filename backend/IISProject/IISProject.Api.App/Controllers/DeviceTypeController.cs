using IISProject.Api.BL.Facades;
using IISProject.Api.BL.Models.DeviceType;
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

    [HttpGet("{id}")]
    public async Task<DeviceTypeDetailModel?> GetDeviceTypeById(Guid id)
    {
        return await _deviceTypeFacade.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<DeviceTypeDetailModel> CreateDevice(DeviceTypeDetailModel deviceType)
    {
        return await _deviceTypeFacade.SaveAsync(deviceType);
    }
    
    [HttpPut("{id}")]
    public async Task<DeviceTypeDetailModel> UpdateDeviceType(Guid id, DeviceTypeDetailModel deviceType)
    {
        deviceType.Id = id;
        return await _deviceTypeFacade.SaveAsync(deviceType);
    }

    [HttpDelete("{id}")]
    public async Task DeleteDeviceType(Guid id)
    {
        await _deviceTypeFacade.DeleteAsync(id);
    }
}