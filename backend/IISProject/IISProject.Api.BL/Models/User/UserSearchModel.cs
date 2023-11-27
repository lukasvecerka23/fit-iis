namespace IISProject.Api.BL.Models.User;

public record UserSearchModel
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public IEnumerable<UserListModel> Users { get; set; } = new List<UserListModel>();
}