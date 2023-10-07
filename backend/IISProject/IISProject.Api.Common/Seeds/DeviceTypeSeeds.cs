using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.Common.Seeds;

public static class DeviceTypeSeeds
{
    public static readonly DeviceTypeEntity DefaultDeviceType = new()
    {
        Id = Guid.Parse("C7BEC5C9-736D-4CD3-A75E-BB2F45FBCE51"),
        Name = "Default Device Type",
    };
    
    public static readonly DeviceTypeEntity DeviceTypeToDelete =
        DefaultDeviceType with { Id = Guid.Parse("19A6D003-6AED-483F-B969-C90DF2BDDA08") };

    public static readonly DeviceTypeEntity DeviceTypeToUpdate =
        DefaultDeviceType with { Id = Guid.Parse("F651DB88-714E-40C6-B249-CFCE2356B44E") };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DeviceTypeEntity>().HasData(
            DefaultDeviceType with
            {
                Devices = Array.Empty<DeviceEntity>(), Parameters = Array.Empty<ParameterEntity>()
            },
            DeviceTypeToDelete with
            {
                Devices = Array.Empty<DeviceEntity>(), Parameters = Array.Empty<ParameterEntity>()
            },
            DeviceTypeToUpdate with
            {
                Devices = Array.Empty<DeviceEntity>(), Parameters = Array.Empty<ParameterEntity>()
            }
        );
    }
}