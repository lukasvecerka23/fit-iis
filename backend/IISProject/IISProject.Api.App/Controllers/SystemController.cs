using IISProject.Api.BL.Facades;
using IISProject.Api.BL.Models.Responses;
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

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SystemDetailModel>> GetSystemById(Guid id)
    {
        var result = await _systemFacade.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound(new ErrorModel {Error = $"System with id {id} not found"});
        }
        return result;
    }

    [HttpPost]
    public async Task<ActionResult<IdModel>> CreateSystem(SystemCreateUpdateModel system)
    {
        var result = await _systemFacade.CreateAsync(system);
        return Created($"/api/systems/{result.Id}", result);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<IdModel>> UpdateSystem(Guid id, SystemCreateUpdateModel system)
    {
        var result = await _systemFacade.UpdateAsync(system, id);
        if (result == null)
        {
            return NotFound(new ErrorModel {Error = $"System with id {id} not found"});
        }
        
        return result;
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteSystem(Guid id)
    {
        var result = await _systemFacade.DeleteAsync(id);
        if (!result)
        {
            return NotFound(new ErrorModel {Error = $"System with id {id} not found"});
        }
        
        return Ok();
    }
}