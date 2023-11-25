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
    
    [HttpGet("search")]
    public async Task<ActionResult<MeasurementSearchModel>> Search([FromQuery] SearchMeasurementParams searchParams)
    {
        var result = await _measurementFacade.SearchAsync(searchParams.DeviceId, searchParams.Query, searchParams.PageIndex, searchParams.PageSize);
        return result;
    }
}

public class SearchMeasurementParams
{
    [FromQuery(Name = "parameterId")] public Guid Query { get; set; } = Guid.Empty;
    
    [FromQuery(Name = "deviceId")] public Guid DeviceId { get; set; } = Guid.Empty;

    [FromQuery(Name = "p")] public int PageIndex { get; set; } = 0;

    [FromQuery(Name = "size")] public int PageSize { get; set; } = 10;
}