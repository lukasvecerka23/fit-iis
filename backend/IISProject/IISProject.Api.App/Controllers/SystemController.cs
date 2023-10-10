using IISProject.Api.BL.Facades;
using IISProject.Api.BL.Models.System;
using Microsoft.AspNetCore.Mvc;

namespace IISProject.Controllers;

[ApiController]
[Route("api/systems")]
public class SystemController : ControllerBase
{
    private readonly ILogger<SystemController> _logger;
    private readonly SystemFacade _systemFacade;

    public SystemController(ILogger<SystemController> logger, SystemFacade systemFacade)
    {
        _logger = logger;
        _systemFacade = systemFacade;
    }

    [HttpGet]
    public async Task<IEnumerable<SystemListModel>> GetSystems()
    {
        return await _systemFacade.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<SystemDetailModel?> GetSystemById(Guid id)
    {
        return await _systemFacade.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<SystemDetailModel> CreateSystem(SystemDetailModel system)
    {
        return await _systemFacade.SaveAsync(system);
    }
    
    [HttpPut("{id}")]
    public async Task<SystemDetailModel> UpdateSystem(Guid id, SystemDetailModel system)
    {
        system.Id = id;
        return await _systemFacade.SaveAsync(system);
    }

    [HttpDelete("{id}")]
    public async Task DeleteSystem(Guid id)
    {
        await _systemFacade.DeleteAsync(id);
    }
}