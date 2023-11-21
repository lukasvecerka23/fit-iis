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
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            DefaultUser with {Roles = Array.Empty<RoleOfUserEntity>(), UserInSystems = Array.Empty<UserInSystemEntity>(), Devices = Array.Empty<DeviceEntity>(), Measurements = Array.Empty<MeasurementEntity>(), Kpis = Array.Empty<KpiEntity>()}
        );
    }
}