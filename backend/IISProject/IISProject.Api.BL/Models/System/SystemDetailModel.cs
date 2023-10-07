using IISProject.Api.BL.Models.Device;
using IISProject.Api.BL.Models.User;

namespace IISProject.Api.BL.Models.System;

public record SystemDetailModel : IModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required Guid CreatorId { get; set; }
    
    public ICollection<UserListModel> Users { get; set; } = new List<UserListModel>();
    public ICollection<DeviceListModel> Devices { get; set; } = new List<DeviceListModel>();
}