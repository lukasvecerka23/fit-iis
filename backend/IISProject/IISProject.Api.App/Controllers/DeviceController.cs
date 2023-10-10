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

    [HttpGet]
    public async Task<IEnumerable<DeviceListModel>> GetDevices()
    {
        return await _deviceFacade.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<DeviceDetailModel?> GetDeviceById(Guid id)
    {
        return await _deviceFacade.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<DeviceDetailModel> CreateDevice(DeviceDetailModel device)
    {
        return await _deviceFacade.SaveAsync(device);
    }
    
    [HttpPut("{id}")]
    public async Task<DeviceDetailModel> UpdateDevice(Guid id, DeviceDetailModel device)
    {
        device.Id = id;
        return await _deviceFacade.SaveAsync(device);
    }

    [HttpDelete("{id}")]
    public async Task DeleteDevice(Guid id)
    {
        await _deviceFacade.DeleteAsync(id);
    }
}