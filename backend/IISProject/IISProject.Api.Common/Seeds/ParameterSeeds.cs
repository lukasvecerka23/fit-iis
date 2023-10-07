using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.Common.Seeds;

public static class ParameterSeeds
{
    public static readonly ParameterEntity DefaultParameter = new()
    {
        Id = Guid.Parse("DB90E2B9-DA6F-4DBA-88B6-050EFBC969A3"),
        Name = "DefaultParameter",
        LowerLimit = -10.0,
        UpperLimit = 10.0,
        DeviceTypeId = DeviceTypeSeeds.DefaultDeviceType.Id,
        DeviceType = DeviceTypeSeeds.DefaultDeviceType
    };
    
    public static readonly ParameterEntity ParameterToDelete =
        DefaultParameter with { Id = Guid.Parse("ED71576B-03A8-4A16-874F-6F986F336460") };

    public static readonly ParameterEntity ParameterToUpdate =
        DefaultParameter with { Id = Guid.Parse("6B690634-8049-4602-AE87-387EBD13B87D") };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ParameterEntity>().HasData(
            DefaultParameter with { Measurements = Array.Empty<MeasurementEntity>(), Kpis = Array.Empty<KpiEntity>() },
            ParameterToDelete with { Measurements = Array.Empty<MeasurementEntity>(), Kpis = Array.Empty<KpiEntity>() },
            ParameterToUpdate with { Measurements = Array.Empty<MeasurementEntity>(), Kpis = Array.Empty<KpiEntity>() }
        );
    }
}