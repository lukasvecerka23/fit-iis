using IISProject.Api.BL.Facades;
using IISProject.Api.BL.Models.Measurement;
using IISProject.Api.BL.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace IISProject.Controllers;

[ApiController]
[Route("api/measurements")]
public class MeasurementController : ControllerBase
{
    private readonly ILogger<MeasurementController> _logger;
    private readonly MeasurementFacade _measurementFacade;

    public MeasurementController(ILogger<MeasurementController> logger, MeasurementFacade measurementFacade)
    {
        _logger = logger;
        _measurementFacade = measurementFacade;
    }

    [HttpGet]
    public async Task<IEnumerable<MeasurementListModel>> GetMeasurements()
    {
        return await _measurementFacade.GetAllAsync();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MeasurementDetailModel>> GetMeasurementById(Guid id)
    {
        var result = await _measurementFacade.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound(new ErrorModel {Error = $"Measurement with id {id} not found"});
        }
        return result;
    }

    [HttpPost]
    public async Task<ActionResult<IdModel>> CreateMeasurement(MeasurementCreateUpdateModel measurement)
    {
        var result = await _measurementFacade.CreateAsync(measurement);
        return Created($"/api/measurements/{result.Id}", result);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<IdModel>> UpdateMeasurement(Guid id, MeasurementCreateUpdateModel measurement)
    {
        var result = await _measurementFacade.UpdateAsync(measurement, id);
        if (result == null)
        {
            return NotFound(new ErrorModel {Error = $"Measurement with id {id} not found"});
        }

        return result;
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteMeasurement(Guid id)
    {
        var result = await _measurementFacade.DeleteAsync(id);
        if (!result)
        {
            return NotFound(new ErrorModel {Error = $"Measurement with id {id} not found"});
        }

        return Ok();
    }
}