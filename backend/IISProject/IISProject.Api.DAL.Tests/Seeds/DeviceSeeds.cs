using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.DAL.Tests.Seeds;

public static class DeviceSeeds
{
    public static readonly DeviceEntity DefaultDevice = new()
    {
        Id = Guid.Parse("4804E24D-0D25-4C3F-9F05-74738D5A1806"),
        UserAlias = "Default Device",
        DeviceTypeId = DeviceTypeSeeds.DefaultDeviceType.Id,
        CreatorId = UserSeeds.DefaultUser.Id,
        SystemId = SystemSeeds.DefaultSystem.Id,
        Description = "Default Device Description"
    };

    public static readonly DeviceEntity DeviceToDelete =
        DefaultDevice with { Id = Guid.Parse("F5D37582-5A58-4710-9BCB-7D19DF0606D5") };
    
    public static readonly DeviceEntity DeviceToUpdate =
        DefaultDevice with { Id = Guid.Parse("FF1E7470-E672-410F-97D8-462168BD6866") };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DeviceEntity>().HasData(
            DefaultDevice with { Measurements = Array.Empty<MeasurementEntity>(), Kpis = Array.Empty<KpiEntity>() },
            DeviceToDelete with { Measurements = Array.Empty<MeasurementEntity>(), Kpis = Array.Empty<KpiEntity>() },
            DeviceToUpdate with { Measurements = Array.Empty<MeasurementEntity>(), Kpis = Array.Empty<KpiEntity>() }
            );
    }

}