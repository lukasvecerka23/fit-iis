using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.DAL.Seeds;

public static class ParameterSeeds
{
    public static readonly ParameterEntity DefaultParameter = new()
    {
        Id = Guid.Parse("DB90E2B9-DA6F-4DBA-88B6-050EFBC969A3"),
        Name = "Teplota",
        LowerLimit = -10.0,
        UpperLimit = 10.0,
        DeviceTypeId = DeviceTypeSeeds.DefaultDeviceType.Id
    };
    
    public static readonly ParameterEntity DefaultParameter2 = new()
    {
        Id = Guid.Parse("6BDE820E-211D-46F8-921B-6992B5D5E514"),
        Name = "Vlhkost",
        LowerLimit = -10.0,
        UpperLimit = 10.0,
        DeviceTypeId = DeviceTypeSeeds.DefaultDeviceType.Id
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ParameterEntity>().HasData(
            DefaultParameter with { Measurements = Array.Empty<MeasurementEntity>(), Kpis = Array.Empty<KpiEntity>() },
            DefaultParameter2 with { Measurements = Array.Empty<MeasurementEntity>(), Kpis = Array.Empty<KpiEntity>() }
        );
    }
}