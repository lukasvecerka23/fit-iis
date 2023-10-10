using IISProject.Api.BL.Facades;
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

    [HttpGet("{id}")]
    public async Task<UserInSystemDetailModel?> GetUserInSystemById(Guid id)
    {
        return await _userInSystemFacade.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<UserInSystemDetailModel> CreateUserInSystem(UserInSystemDetailModel userInSystem)
    {
        return await _userInSystemFacade.SaveAsync(userInSystem);
    }
    
    [HttpPut("{id}")]
    public async Task<UserInSystemDetailModel> UpdateUserInSystem(Guid id, UserInSystemDetailModel userInSystem)
    {
        userInSystem.Id = id;
        return await _userInSystemFacade.SaveAsync(userInSystem);
    }

    [HttpDelete("{id}")]
    public async Task DeleteUserInSystem(Guid id)
    {
        await _userInSystemFacade.DeleteAsync(id);
    }
}