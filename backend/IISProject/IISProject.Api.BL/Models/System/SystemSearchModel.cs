namespace IISProject.Api.BL.Models.System;

public record SystemSearchModel
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public IEnumerable<SystemListModel> Systems { get; set; } = new List<SystemListModel>();
}