using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.DAL.Seeds;

public static class DeviceSeeds
{
    public static readonly DeviceEntity DefaultDevice = new()
    {
        Id = Guid.Parse("4804E24D-0D25-4C3F-9F05-74738D5A1806"),
        UserAlias = "Senzor teploty - kuchyň vchod",
        DeviceTypeId = DeviceTypeSeeds.DefaultDeviceType.Id,
        CreatorId = UserSeeds.DefaultUser.Id,
        SystemId = SystemSeeds.DefaultSystem.Id,
        Description = "Default Device Description",
        UserId = "KUCHYN_VCHOD01"
    };
    
    public static readonly DeviceEntity DefaultDevice2 = new()
    {
        Id = Guid.Parse("BD558FF1-B26E-4FB2-8A07-09568B63FFF6"),
        UserAlias = "Senzor teploty - lednice",
        DeviceTypeId = DeviceTypeSeeds.DefaultDeviceType.Id,
        CreatorId = UserSeeds.DefaultUser.Id,
        SystemId = SystemSeeds.DefaultSystem.Id,
        Description = "Default Device Description 2",
        UserId = "KUCH_LEDNICE01"
    };
    
    public static readonly DeviceEntity DefaultDevice3 = new()
    {
        Id = Guid.Parse("BFE2308D-7151-4C19-AFAA-0BB15389D62E"),
        UserAlias = "Teploměr - dvůr",
        DeviceTypeId = DeviceTypeSeeds.DefaultDeviceType.Id,
        CreatorId = UserSeeds.DefaultUser.Id,
        SystemId = SystemSeeds.DefaultSystem2.Id,
        Description = "Default Device Description 3",
        UserId = "DVUR01"
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DeviceEntity>().HasData(
            DefaultDevice with { Measurements = Array.Empty<MeasurementEntity>(), Kpis = Array.Empty<KpiEntity>() },
            DefaultDevice2 with { Measurements = Array.Empty<MeasurementEntity>(), Kpis = Array.Empty<KpiEntity>() },
            DefaultDevice3 with { Measurements = Array.Empty<MeasurementEntity>(), Kpis = Array.Empty<KpiEntity>() }
            );
    }

}