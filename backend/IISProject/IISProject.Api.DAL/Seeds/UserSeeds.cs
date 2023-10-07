using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.DAL.Seeds;

public static class UserSeeds
{
    public static readonly UserEntity DefaultUser = new()
    {
        Id = Guid.Parse("F6E282C7-20E0-4AF1-B531-18C70D0C53D2"),
        Name = "John",
        Surname = "Doe",
        Email = "johndoe@email.email"
    };
    
    public static readonly UserEntity UserToDelete =
        DefaultUser with { Id = Guid.Parse("9B2ECD8D-D7A9-4194-917D-34D67E79E4F0") };
    
    public static readonly UserEntity UserToUpdate = DefaultUser with { Id = Guid.Parse("7AE65DF4-42F4-4679-8320-C1684B438BCB") };
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            DefaultUser with {Roles = Array.Empty<RoleOfUserEntity>(), UserInSystems = Array.Empty<UserInSystemEntity>(), Devices = Array.Empty<DeviceEntity>(), Measurements = Array.Empty<MeasurementEntity>(), Kpis = Array.Empty<KpiEntity>()},
            UserToDelete with {Roles = Array.Empty<RoleOfUserEntity>(), UserInSystems = Array.Empty<UserInSystemEntity>(), Devices = Array.Empty<DeviceEntity>(), Measurements = Array.Empty<MeasurementEntity>(), Kpis = Array.Empty<KpiEntity>()},
            UserToUpdate with {Roles = Array.Empty<RoleOfUserEntity>(), UserInSystems = Array.Empty<UserInSystemEntity>(), Devices = Array.Empty<DeviceEntity>(), Measurements = Array.Empty<MeasurementEntity>(), Kpis = Array.Empty<KpiEntity>()}
        );
    }
}