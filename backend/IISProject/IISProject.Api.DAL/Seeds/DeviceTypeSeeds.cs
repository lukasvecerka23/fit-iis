using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.DAL.Seeds;

public static class DeviceTypeSeeds
{
    public static readonly DeviceTypeEntity DefaultDeviceType = new()
    {
        Id = Guid.Parse("C7BEC5C9-736D-4CD3-A75E-BB2F45FBCE51"),
        Name = "Senzor teploty",
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DeviceTypeEntity>().HasData(
            DefaultDeviceType with
            {
                Devices = Array.Empty<DeviceEntity>(), Parameters = Array.Empty<ParameterEntity>()
            }
        );
    }
}