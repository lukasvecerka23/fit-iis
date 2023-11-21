using IISProject.Api.BL.Facades;
using IISProject.Api.BL.Models.Responses;
using IISProject.Api.BL.Models.RoleOfUser;
using Microsoft.AspNetCore.Mvc;

namespace IISProject.Controllers;

[ApiController]
[Route("api/roleOfUsers")]
public class RoleOfUserController : ControllerBase
{
    private readonly ILogger<RoleOfUserController> _logger;
    private readonly RoleOfUserFacade _roleOfUserFacade;

    public RoleOfUserController(ILogger<RoleOfUserController> logger, RoleOfUserFacade roleOfUserFacade)
    {
        _logger = logger;
        _roleOfUserFacade = roleOfUserFacade;
    }

    [HttpGet]
    public async Task<IEnumerable<RoleOfUserListModel>> GetRoleOfUser()
    {
        return await _roleOfUserFacade.GetAllAsync();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RoleOfUserDetailModel>> GetRoleOfUserById(Guid id)
    {
        var result = await _roleOfUserFacade.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound(new ErrorModel {Error = $"RoleOfUser with id {id} not found"});
        }
        return result;
    }

    [HttpPost]
    public async Task<ActionResult<IdModel>> CreateRoleOfUser(RoleOfUserCreateUpdateModel roleOfUser)
    {
        var result = await _roleOfUserFacade.CreateAsync(roleOfUser);
        return Created($"/api/roleOfUsers/{result.Id}", result);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<IdModel>> UpdateRoleOfUser(Guid id, RoleOfUserCreateUpdateModel roleOfUser)
    {
        var result = await _roleOfUserFacade.UpdateAsync(roleOfUser, id);
        if (result == null)
        {
            return NotFound(new ErrorModel {Error = $"RoleOfUser with id {id} not found"});
        }
        
        return result;
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteRoleOfUser(Guid id)
    {
        var result = await _roleOfUserFacade.DeleteAsync(id);
        if (!result)
        {
            return NotFound(new ErrorModel {Error = $"RoleOfUser with id {id} not found"});
        }

        return Ok();
    }
}