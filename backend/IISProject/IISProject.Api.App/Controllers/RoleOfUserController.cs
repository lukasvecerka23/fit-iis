using IISProject.Api.BL.Facades;
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

    [HttpGet("{id}")]
    public async Task<RoleOfUserDetailModel?> GetRoleOfUserById(Guid id)
    {
        return await _roleOfUserFacade.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<RoleOfUserDetailModel> CreateRoleOfUser(RoleOfUserDetailModel roleOfUser)
    {
        return await _roleOfUserFacade.SaveAsync(roleOfUser);
    }
    
    [HttpPut("{id}")]
    public async Task<RoleOfUserDetailModel> UpdateRoleOfUser(Guid id, RoleOfUserDetailModel roleOfUser)
    {
        roleOfUser.Id = id;
        return await _roleOfUserFacade.SaveAsync(roleOfUser);
    }

    [HttpDelete("{id}")]
    public async Task DeleteRoleOfUser(Guid id)
    {
        await _roleOfUserFacade.DeleteAsync(id);
    }
}