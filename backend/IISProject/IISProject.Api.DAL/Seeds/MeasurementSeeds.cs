using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.DAL.Seeds;

public static class MeasurementSeeds
{
    public static readonly MeasurementEntity DefaultMeasurement = new()
    {
        Id = Guid.Parse("50F9A54F-F463-4D0D-9AC4-90CAF3E0908D"),
        Value = 10.0,
        TimeStamp = DateTime.Parse("2021-10-10T10:10:10.0000000"),
        DeviceId = DeviceSeeds.DefaultDevice.Id,
        ParameterId = ParameterSeeds.DefaultParameter.Id
    };
    
    public static readonly MeasurementEntity DefaultMeasurement2 = new()
    {
        Id = Guid.Parse("80BFCE67-DCF9-4844-978B-9B377948EC07"),
        Value = 12.5,
        TimeStamp = DateTime.Parse("2021-9-10T10:10:10.0000000"),
        DeviceId = DeviceSeeds.DefaultDevice.Id,
        ParameterId = ParameterSeeds.DefaultParameter.Id
    };
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MeasurementEntity>().HasData(
            DefaultMeasurement,
            DefaultMeasurement2
        );
    }
}