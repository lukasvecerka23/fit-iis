namespace IISProject.Api.BL.Models.DeviceType;

public record DeviceTypeSearchModel
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public IEnumerable<DeviceTypeListModel> DeviceTypes { get; set; } = new List<DeviceTypeListModel>();
}