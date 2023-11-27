using IISProject.Api.BL.Facades;
using IISProject.Api.BL.Models.Responses;
using IISProject.Api.BL.Models.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

    [Authorize]
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

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<IdModel>> CreateSystem(SystemCreateUpdateModel system)
    {
        var userId = Request.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return NotFound();
        }
        var result = await _systemFacade.CreateAsync(system, Guid.Parse(userId));
        return Created($"/api/systems/{result.Id}", result);
    }
    
    [Authorize]
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

    [Authorize]
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
    
    [HttpGet("search")]
    public async Task<ActionResult<SystemSearchModel>> Search([FromQuery] SearchSystemParams searchParams)
    {
        var result = await _systemFacade.SearchAsync(searchParams);
        return result;
    }
    
    [HttpPost("{id:guid}/leave")]
    public async Task<ActionResult> LeaveSystem(Guid id)
    {
        var result = await _systemFacade.LeaveSystemAsync(id);
        
        if (!result)
        {
            return NotFound(new ErrorModel {Error = $"System with id {id} not found"});
        }
        
        return Ok();
    }
}