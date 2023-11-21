using IISProject.Api.BL.Facades;
using IISProject.Api.BL.Models.Responses;
using IISProject.Api.BL.Models.UserInSystem;
using Microsoft.AspNetCore.Mvc;

namespace IISProject.Controllers;

[ApiController]
[Route("api/userInSystems")]
public class UserInSystemController : ControllerBase
{
    private readonly ILogger<UserInSystemController> _logger;
    private readonly UserInSystemFacade _userInSystemFacade;

    public UserInSystemController(ILogger<UserInSystemController> logger, UserInSystemFacade userInSystemFacade)
    {
        _logger = logger;
        _userInSystemFacade = userInSystemFacade;
    }

    [HttpGet]
    public async Task<IEnumerable<UserInSystemListModel>> GetUserInSystems()
    {
        return await _userInSystemFacade.GetAllAsync();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserInSystemDetailModel>> GetUserInSystemById(Guid id)
    {
        var result = await _userInSystemFacade.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound(new ErrorModel {Error = $"UserInSystem with id {id} not found"});
        }
        return result;
    }

    [HttpPost]
    public async Task<ActionResult<IdModel>> CreateUserInSystem(UserInSystemCreateUpdateModel userInSystem)
    {
        var result = await _userInSystemFacade.CreateAsync(userInSystem);
        return Created($"/api/userInSystems/{result.Id}", result);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<IdModel>> UpdateUserInSystem(Guid id, UserInSystemCreateUpdateModel userInSystem)
    {
        var result = await _userInSystemFacade.UpdateAsync(userInSystem, id);
        if (result == null)
        {
            return NotFound(new ErrorModel {Error = $"UserInSystem with id {id} not found"});
        }
        
        return result;
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteUserInSystem(Guid id)
    {
        var result = await _userInSystemFacade.DeleteAsync(id);
        if (!result)
        {
            return NotFound(new ErrorModel {Error = $"UserInSystem with id {id} not found"});
        }
        
        return Ok();
    }
}