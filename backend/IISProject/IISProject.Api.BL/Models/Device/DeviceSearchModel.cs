namespace IISProject.Api.BL.Models.Device;

public record DeviceSearchModel
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public IEnumerable<DeviceListModel> Devices { get; set; } = new List<DeviceListModel>();
}