using IISProject.Api.BL.Facades;
using IISProject.Api.BL.Models.Responses;
using IISProject.Api.BL.Models.Role;
using Microsoft.AspNetCore.Mvc;

namespace IISProject.Controllers;

[ApiController]
[Route("api/roles")]
public class RoleController : ControllerBase
{
    private readonly ILogger<RoleController> _logger;
    private readonly RoleFacade _roleFacade;

    public RoleController(ILogger<RoleController> logger, RoleFacade roleFacade)
    {
        _logger = logger;
        _roleFacade = roleFacade;
    }

    [HttpGet]
    public async Task<IEnumerable<RoleListModel>> GetRoles()
    {
        return await _roleFacade.GetAllAsync();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RoleDetailModel>> GetRoleById(Guid id)
    {
        var result = await _roleFacade.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound(new ErrorModel {Error = $"Role with id {id} not found"});
        }
        return result;
    }

    [HttpPost]
    public async Task<ActionResult<IdModel>> CreateRole(RoleCreateUpdateModel role)
    {
        var result = await _roleFacade.CreateAsync(role);
        return Created($"/api/roles/{result.Id}", result);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<IdModel>> UpdateRole(Guid id, RoleCreateUpdateModel role)
    {   
        var result = await _roleFacade.UpdateAsync(role, id);
        if (result == null)
        {
            return NotFound(new ErrorModel {Error = $"Role with id {id} not found"});
        }
        
        return result;
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteRole(Guid id)
    {   
        var result = await _roleFacade.DeleteAsync(id);
        if (!result)
        {
            return NotFound(new ErrorModel {Error = $"Role with id {id} not found"});
        }
        
        return Ok();
    }
}