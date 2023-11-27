using IISProject.Api.BL.Facades;
using IISProject.Api.BL.Models.AssignToSystem;
using IISProject.Api.BL.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace IISProject.Controllers;

[ApiController]
[Route("api/assignToSystem")]
public class AssignToSystemController: ControllerBase
{
    private readonly AssignToSystemFacade _assignToSystemFacade;

    public AssignToSystemController(AssignToSystemFacade assignToSystemFacade)
    {
        _assignToSystemFacade = assignToSystemFacade;
    }
    
    [HttpGet]
    public async Task<IEnumerable<AssignToSystemListModel>> GetAssignToSystems([FromQuery] AssignToSystemParams parameters)
    {
        return await _assignToSystemFacade.GetAllAsync(parameters);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AssignToSystemDetailModel>> GetAssignToSystemById(Guid id)
    {
        var result = await _assignToSystemFacade.GetByIdAsync(id);
        
        if (result == null)
        {
            return NotFound();
        }

        return result;
    }
    
    [HttpPost]
    public async Task<ActionResult<IdModel>> CreateAssignToSystem(AssignToSystemCreateUpdateModel assignToSystem)
    {
        var result = await _assignToSystemFacade.CreateAsync(assignToSystem);
        return Created($"/api/assignToSystem/{result.Id}", result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<IdModel>> UpdateDevice(Guid id, AssignToSystemCreateUpdateModel assignToSystem)
    {

        var result = await _assignToSystemFacade.UpdateAsync(assignToSystem, id);

        if (result == null)
        {
            return NotFound(new ErrorModel {Error = $"AssignToSystem with id {id} not found"});
        }
        
        return result;
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteDevice(Guid id)
    {
        var result = await _assignToSystemFacade.DeleteAsync(id);
        
        if (!result)
        {
            return NotFound(new ErrorModel {Error = $"AssignToSystem with id {id} not found"});
        }

        return Ok();
    }
    
    [HttpPost("{id:guid}/accept")]
    public async Task<ActionResult> AssignUserToSystem(Guid id)
    {
        var result = await _assignToSystemFacade.AssignUserToSystem(id);
        
        if (!result)
        {
            return NotFound(new ErrorModel {Error = $"AssignToSystem with id {id} not found"});
        }

        return Ok();
    }
}