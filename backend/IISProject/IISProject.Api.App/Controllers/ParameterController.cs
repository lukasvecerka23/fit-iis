using IISProject.Api.BL.Facades;
using IISProject.Api.BL.Models.Parameter;
using IISProject.Api.BL.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace IISProject.Controllers;

[ApiController]
[Route("api/parameters")]
public class ParameterController : ControllerBase
{
    private readonly ILogger<ParameterController> _logger;
    private readonly ParameterFacade _parameterFacade;

    public ParameterController(ILogger<ParameterController> logger, ParameterFacade parameterFacade)
    {
        _logger = logger;
        _parameterFacade = parameterFacade;
    }

    [HttpGet]
    public async Task<IEnumerable<ParameterListModel>> GetParameters()
    {
        return await _parameterFacade.GetAllAsync();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ParameterDetailModel>> GetParameterById(Guid id)
    {
        var result = await _parameterFacade.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound(new ErrorModel { Error = $"Parameter with id {id} not found " });
        }
        return result;
    }

    [HttpPost]
    public async Task<ActionResult<IdModel>> CreateParameter(ParameterCreateUpdateModel parameter)
    {
        var result = await _parameterFacade.CreateAsync(parameter);
        return Created($"/api/parameters/{result.Id}", result);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<IdModel>> UpdateParameter(Guid id, ParameterCreateUpdateModel parameter)
    {
        var result = await _parameterFacade.UpdateAsync(parameter, id);
        if (result == null)
        {
            return NotFound(new ErrorModel {Error = $"Parameter with id {id} not found"});
        }
        
        return result;   
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteParameter(Guid id)
    {
        var result = await _parameterFacade.DeleteAsync(id);
        if (!result)
        {
            return NotFound(new ErrorModel {Error = $"Parameter with id {id} not found"});
        }

        return Ok();
    }
    
    [HttpGet("status")]
    public async Task<ActionResult<IEnumerable<ParameterStatusListModel>>> GetParametersStatus([FromQuery] ParameterStatusParams parameters)
    {
        if (parameters.DeviceTypeId == Guid.Empty || parameters.DeviceId == Guid.Empty)
        {
            return BadRequest();
        }
        
        return Ok(await _parameterFacade.GetParameterStatus(parameters.DeviceTypeId, parameters.DeviceId));
    }
}

public class ParameterStatusParams
{
    [FromQuery(Name = "deviceTypeId")] public Guid DeviceTypeId { get; set; } = Guid.Empty;
    
    [FromQuery(Name = "deviceId")] public Guid DeviceId { get; set; } = Guid.Empty;
}