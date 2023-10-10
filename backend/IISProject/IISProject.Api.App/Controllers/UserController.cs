using IISProject.Api.BL.Facades;
using IISProject.Api.BL.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace IISProject.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly UserFacade _userFacade;

    public UserController(ILogger<UserController> logger, UserFacade userFacade)
    {
        _logger = logger;
        _userFacade = userFacade;
    }

    [HttpGet]
    public async Task<IEnumerable<UserListModel>> GetUsers()
    {
        return await _userFacade.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<UserDetailModel?> GetUserById(Guid id)
    {
        return await _userFacade.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<UserDetailModel> CreateUser(UserDetailModel user)
    {
        return await _userFacade.SaveAsync(user);
    }
    
    [HttpPut("{id}")]
    public async Task<UserDetailModel> UpdateUser(Guid id, UserDetailModel user)
    {
        user.Id = id;
        return await _userFacade.SaveAsync(user);
    }

    [HttpDelete("{id}")]
    public async Task DeleteUser(Guid id)
    {
        await _userFacade.DeleteAsync(id);
    }
}