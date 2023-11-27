using IISProject.Api.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.DAL.Tests.Seeds;

public static class UserSeeds
{
    public static readonly UserEntity DefaultUser = new()
    {
        Id = Guid.Parse("F6E282C7-20E0-4AF1-B531-18C70D0C53D2"),
        Name = "John",
        Surname = "Doe",
        Username = "johndoe",
        PasswordHash = GeneratePasswordHash("test123"),
        RoleId = RoleSeeds.DefaultRole.Id
    };
    
    private static string GeneratePasswordHash(string password)
    {
        var passwordHasher = new PasswordHasher<UserEntity>();
        return passwordHasher.HashPassword(DefaultUser, password);
    }
    
    public static readonly UserEntity UserToDelete =
        DefaultUser with { Id = Guid.Parse("9B2ECD8D-D7A9-4194-917D-34D67E79E4F0") };
    
    public static readonly UserEntity UserToUpdate = DefaultUser with { Id = Guid.Parse("7AE65DF4-42F4-4679-8320-C1684B438BCB") };
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            DefaultUser with {UserInSystems = Array.Empty<UserInSystemEntity>(), Devices = Array.Empty<DeviceEntity>(), AssignsToSystems = Array.Empty<AssignToSystemEntity>(), Kpis = Array.Empty<KpiEntity>()},
            UserToDelete with {UserInSystems = Array.Empty<UserInSystemEntity>(), Devices = Array.Empty<DeviceEntity>(), AssignsToSystems = Array.Empty<AssignToSystemEntity>(), Kpis = Array.Empty<KpiEntity>()},
            UserToUpdate with {UserInSystems = Array.Empty<UserInSystemEntity>(), Devices = Array.Empty<DeviceEntity>(), AssignsToSystems = Array.Empty<AssignToSystemEntity>(), Kpis = Array.Empty<KpiEntity>()}
        );
    }
}