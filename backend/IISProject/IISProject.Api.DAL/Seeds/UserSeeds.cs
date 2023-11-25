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
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            DefaultUser with {UserInSystems = Array.Empty<UserInSystemEntity>(), Devices = Array.Empty<DeviceEntity>(), Measurements = Array.Empty<MeasurementEntity>(), Kpis = Array.Empty<KpiEntity>()},
            AdminUser with {UserInSystems = Array.Empty<UserInSystemEntity>(), Devices = Array.Empty<DeviceEntity>(), Measurements = Array.Empty<MeasurementEntity>(), Kpis = Array.Empty<KpiEntity>()},
            BrokerUser with {UserInSystems = Array.Empty<UserInSystemEntity>(), Devices = Array.Empty<DeviceEntity>(), Measurements = Array.Empty<MeasurementEntity>(), Kpis = Array.Empty<KpiEntity>()}
        );
    }
}