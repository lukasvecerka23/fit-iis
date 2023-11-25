using IISProject.Api.Common.Enum;
using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.DAL.Seeds;

public static class KpiSeeds
{
    public static readonly KpiEntity DefaultKpi = new()
    {
        Id = Guid.Parse("4238CA20-DCAC-4319-A9C7-B42C1169EB22"),
        Function = KpiFunction.Greater,
        Error = false,
        CreatorId = UserSeeds.DefaultUser.Id,
        DeviceId = DeviceSeeds.DefaultDevice.Id,
        ParameterId = ParameterSeeds.DefaultParameter.Id,
        Value = 30
    };
    
    public static readonly KpiEntity DefaultKpi2 = new()
    {
        Id = Guid.Parse("8DA3A5AC-9213-442A-996F-EA731E0F339E"),
        Function = KpiFunction.Less,
        Error = true,
        CreatorId = UserSeeds.DefaultUser.Id,
        DeviceId = DeviceSeeds.DefaultDevice.Id,
        ParameterId = ParameterSeeds.DefaultParameter2.Id,
        Value = -5
    };
    
    public static readonly KpiEntity DefaultKpi3 = new()
    {
        Id = Guid.Parse("6E036326-4D39-4B3F-BD16-532177299A44"),
        Function = KpiFunction.NotEqual,
        Error = true,
        CreatorId = UserSeeds.DefaultUser.Id,
        DeviceId = DeviceSeeds.DefaultDevice.Id,
        ParameterId = ParameterSeeds.DefaultParameter2.Id,
        Value = 10
    };
    
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KpiEntity>().HasData(
            DefaultKpi,
            DefaultKpi2,
            DefaultKpi3
        );
    }
}