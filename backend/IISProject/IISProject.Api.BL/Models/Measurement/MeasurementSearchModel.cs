namespace IISProject.Api.BL.Models.Measurement;

public record MeasurementSearchModel
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public IEnumerable<MeasurementListModel> Measurements { get; set; } = new List<MeasurementListModel>();
}