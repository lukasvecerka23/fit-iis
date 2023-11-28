using IISProject.Api.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.DAL.Seeds;

public static class UserSeeds
{

    public static readonly UserEntity DefaultUser = new()
    {
        Id = Guid.Parse("F6E282C7-20E0-4AF1-B531-18C70D0C53D2"),
        Name = "John",
        Surname = "Doe",
        Username = "johndoe",
        PasswordHash = GeneratePasswordHash("test123"),
        RoleId = RoleSeeds.UserRole.Id
    };
    
    public static readonly UserEntity DefaultUser2 = new()
    {
        Id = Guid.Parse("C3D2EC68-7CAB-4A1A-A1AD-F30D0110A287"),
        Name = "Iron",
        Surname = "Man",
        Username = "ironmanisawesome",
        PasswordHash = GeneratePasswordHash("test123"),
        RoleId = RoleSeeds.UserRole.Id
    };
    
    public static readonly UserEntity AdminUser = new()
    {
        Id = Guid.Parse("F6E282C7-20E0-4AF1-B531-18C70D0C53D3"),
        Name = "Admin",
        Surname = "Admin",
        Username = "admin",
        PasswordHash = GeneratePasswordHash("admin"),
        RoleId = RoleSeeds.AdminRole.Id
    };
    
    public static readonly UserEntity BrokerUser = new()
    {
        Id = Guid.Parse("F6E282C7-20E0-4AF1-B531-18C70D0C53D4"),
        Name = "Broker",
        Surname = "Broker",
        Username = "broker",
        PasswordHash = GeneratePasswordHash("broker"),
        RoleId = RoleSeeds.BrokerRole.Id
    };
    
    private static string GeneratePasswordHash(string password)
    {
        var passwordHasher = new PasswordHasher<UserEntity>();
        return passwordHasher.HashPassword(DefaultUser, password);
    }
    
    public static IEnumerable<UserEntity> GetDefaultUsers()
    {
        return new List<UserEntity>
        {
            DefaultUser with {UserInSystems = new List<UserInSystemEntity>(), Devices = new List<DeviceEntity>(), Kpis = new List<KpiEntity>(), AssignsToSystems = new List<AssignToSystemEntity>()},
            AdminUser with {UserInSystems = new List<UserInSystemEntity>(), Devices = new List<DeviceEntity>(), Kpis = new List<KpiEntity>(), AssignsToSystems = new List<AssignToSystemEntity>()},
            BrokerUser with {UserInSystems = new List<UserInSystemEntity>(), Devices = new List<DeviceEntity>(), Kpis = new List<KpiEntity>(), AssignsToSystems = new List<AssignToSystemEntity>()},
            DefaultUser2 with {UserInSystems = new List<UserInSystemEntity>(), Devices = new List<DeviceEntity>(), Kpis = new List<KpiEntity>(), AssignsToSystems = new List<AssignToSystemEntity>()}
        };
    }
}