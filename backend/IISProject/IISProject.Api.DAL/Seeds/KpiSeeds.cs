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
        ParameterId = ParameterSeeds.DefaultParameter.Id
    };
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KpiEntity>().HasData(
            DefaultKpi
        );
    }
}