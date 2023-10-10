using IISProject.Api.BL.Facades;
using IISProject.Api.BL.Models.Measurement;
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

    [HttpGet("{id}")]
    public async Task<MeasurementDetailModel?> GetMeasurementById(Guid id)
    {
        return await _measurementFacade.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<MeasurementDetailModel> CreateMeasurement(MeasurementDetailModel measurement)
    {
        return await _measurementFacade.SaveAsync(measurement);
    }
    
    [HttpPut("{id}")]
    public async Task<MeasurementDetailModel> UpdateMeasurement(Guid id, MeasurementDetailModel measurement)
    {
        measurement.Id = id;
        return await _measurementFacade.SaveAsync(measurement);
    }

    [HttpDelete("{id}")]
    public async Task DeleteMeasurement(Guid id)
    {
        await _measurementFacade.DeleteAsync(id);
    }
}