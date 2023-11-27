using IISProject.Api.BL.Facades;
using IISProject.Api.BL.Models.Responses;
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

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDetailModel>> GetUserById(Guid id)
    {
        var result = await _userFacade.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound(new ErrorModel {Error = $"User with id {id} not found"});
        }
        return result;
    }

    [HttpPost]
    public async Task<ActionResult<IdModel>> CreateUser(UserCreateUpdateModel user)
    {
        var result = await _userFacade.CreateAsync(user);
        if (result == null)
        {
            return BadRequest(new ErrorModel {Error = "User with this username already exists"});
        }
        return Created($"/api/users/{result.Id}", result);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<IdModel>> UpdateUser(Guid id, UserCreateUpdateModel user)
    {
        var result = await _userFacade.UpdateAsync(user, id);
        if (result == null)
        {
            return NotFound(new ErrorModel {Error = $"User with id {id} not found"});
        }
        
        return result;
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteUser(Guid id)
    {
        var result = await _userFacade.DeleteAsync(id);
        
        if (!result)
        {
            return NotFound(new ErrorModel {Error = $"User with id {id} not found"});
        }

        return Ok();
    }
    
    [HttpGet("search")]
    public async Task<ActionResult<UserSearchModel>> Search([FromQuery] SearchUserParams searchParams)
    {
        var result = await _userFacade.SearchAsync(searchParams.Query, searchParams.PageIndex, searchParams.PageSize);
        return result;
    }
}

public class SearchUserParams
{
    [FromQuery(Name = "q")] public string Query { get; set; } = "";

    [FromQuery(Name = "p")] public int PageIndex { get; set; } = 0;

    [FromQuery(Name = "size")] public int PageSize { get; set; } = 10;
}