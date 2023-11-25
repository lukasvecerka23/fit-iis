using IISProject.Api.BL.Models.Device;
using IISProject.Api.BL.Models.Kpi;
using IISProject.Api.BL.Models.UserInSystem;

namespace IISProject.Api.BL.Models.User;

public record UserDetailModel: IModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Username { get; set; }
    
    public required Guid RoleId { get; set; }
    
    public required string RoleName { get; set; }
}