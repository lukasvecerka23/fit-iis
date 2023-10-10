using IISProject.Api.BL.Facades;
using IISProject.Api.BL.Models.Parameter;
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

    [HttpGet("{id}")]
    public async Task<ParameterDetailModel?> GetParameterById(Guid id)
    {
        return await _parameterFacade.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<ParameterDetailModel> CreateParameter(ParameterDetailModel parameter)
    {
        return await _parameterFacade.SaveAsync(parameter);
    }
    
    [HttpPut("{id}")]
    public async Task<ParameterDetailModel> UpdateParameter(Guid id, ParameterDetailModel parameter)
    {
        parameter.Id = id;
        return await _parameterFacade.SaveAsync(parameter);
    }

    [HttpDelete("{id}")]
    public async Task DeleteParameter(Guid id)
    {
        await _parameterFacade.DeleteAsync(id);
    }
}