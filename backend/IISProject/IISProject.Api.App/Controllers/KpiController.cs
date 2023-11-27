using IISProject.Api.BL.Facades;
using IISProject.Api.BL.Models.Kpi;
using IISProject.Api.BL.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace IISProject.Controllers;

[ApiController]
[Route("api/kpis")]
public class KpiController : ControllerBase
{
    private readonly ILogger<KpiController> _logger;
    private readonly KpiFacade _kpiFacade;

    public KpiController(ILogger<KpiController> logger, KpiFacade kpiFacade)
    {
        _logger = logger;
        _kpiFacade = kpiFacade;
    }

    [HttpGet]
    public async Task<IEnumerable<KpiListModel>> GetKpis()
    {
        return await _kpiFacade.GetAllAsync();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<KpiDetailModel>> GetKpiById(Guid id)
    {
        var result = await _kpiFacade.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound(new ErrorModel {Error = $"Kpi with id {id} not found"});
        }
        return result;
    }

    [HttpPost]
    public async Task<ActionResult<IdModel>> CreateDevice(KpiCreateUpdateModel kpi)
    {
        var result = await _kpiFacade.CreateAsync(kpi);
        return Created($"/api/kpis/{result.Id}", result);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<IdModel>> UpdateKpi(Guid id, KpiCreateUpdateModel kpi)
    {
        var result = await _kpiFacade.UpdateAsync(kpi, id);
        if (result == null)
        {
            return NotFound(new ErrorModel {Error = $"Kpi with id {id} not found"});
        }
        
        return result;
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteKpi(Guid id)
    {
        var result = await _kpiFacade.DeleteAsync(id);
        
        if (!result)
        {
            return NotFound(new ErrorModel {Error = $"Kpi with id {id} not found"});
        }

        return Ok();
    }
    
    [HttpGet("search")]
    public async Task<ActionResult<KpiSearchModel>> Search([FromQuery] SearchKpiParams searchParams)
    {
        var result = await _kpiFacade.SearchAsync(searchParams.DeviceId, searchParams.Query, searchParams.PageIndex, searchParams.PageSize);
        return result;
    }
}

public class SearchKpiParams
{
    [FromQuery(Name = "parameterId")] public Guid Query { get; set; } = Guid.Empty;
    
    [FromQuery(Name = "deviceId")] public Guid DeviceId { get; set; } = Guid.Empty;

    [FromQuery(Name = "p")] public int PageIndex { get; set; } = 0;

    [FromQuery(Name = "size")] public int PageSize { get; set; } = 10;
}