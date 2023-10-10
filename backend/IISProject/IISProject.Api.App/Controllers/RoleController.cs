using IISProject.Api.BL.Facades;
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

    [HttpGet("{id}")]
    public async Task<RoleDetailModel?> GetRoleById(Guid id)
    {
        return await _roleFacade.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<RoleDetailModel> CreateRole(RoleDetailModel role)
    {
        return await _roleFacade.SaveAsync(role);
    }
    
    [HttpPut("{id}")]
    public async Task<RoleDetailModel> UpdateRole(Guid id, RoleDetailModel role)
    {
        role.Id = id;
        return await _roleFacade.SaveAsync(role);
    }

    [HttpDelete("{id}")]
    public async Task DeleteRole(Guid id)
    {
        await _roleFacade.DeleteAsync(id);
    }
}