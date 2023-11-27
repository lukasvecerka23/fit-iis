namespace IISProject.Api.BL.Models.Kpi;

public record KpiSearchModel
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public IEnumerable<KpiListModel> Kpis { get; set; } = new List<KpiListModel>();
}