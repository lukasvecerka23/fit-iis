using IISProject.Api.BL.Models.Device;
using IISProject.Api.BL.Models.Kpi;
using IISProject.Api.BL.Models.RoleOfUser;
using IISProject.Api.BL.Models.UserInSystem;

namespace IISProject.Api.BL.Models.User;

public record UserDetailModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    
    public ICollection<RoleOfUserListModel> RoleOfUsers { get; set; } = new List<RoleOfUserListModel>();
    public ICollection<UserInSystemListModel> Systems { get; set; } = new List<UserInSystemListModel>();
    public ICollection<KpiListModel> Kpis { get; set; } = new List<KpiListModel>();
    public ICollection<DeviceListModel> Devices { get; set; } = new List<DeviceListModel>();
}