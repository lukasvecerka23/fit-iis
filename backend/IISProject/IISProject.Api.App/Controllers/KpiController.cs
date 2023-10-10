using IISProject.Api.BL.Facades;
using IISProject.Api.BL.Models.Kpi;
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

    [HttpGet("{id}")]
    public async Task<KpiDetailModel?> GetKpiById(Guid id)
    {
        return await _kpiFacade.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<KpiDetailModel> CreateDevice(KpiDetailModel kpi)
    {
        return await _kpiFacade.SaveAsync(kpi);
    }
    
    [HttpPut("{id}")]
    public async Task<KpiDetailModel> UpdateKpi(Guid id, KpiDetailModel kpi)
    {
        kpi.Id = id;
        return await _kpiFacade.SaveAsync(kpi);
    }

    [HttpDelete("{id}")]
    public async Task DeleteKpi(Guid id)
    {
        await _kpiFacade.DeleteAsync(id);
    }
}