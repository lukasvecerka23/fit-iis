using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.Common.Seeds;

public static class KpiSeeds
{
    public static readonly KpiEntity DefaultKpi = new()
    {
        Id = Guid.Parse("4238CA20-DCAC-4319-A9C7-B42C1169EB22"),
        Function = "Default Function",
        Error = false,
        CreatorId = UserSeeds.DefaultUser.Id,
        Creator = UserSeeds.DefaultUser,
        DeviceId = DeviceSeeds.DefaultDevice.Id,
        Device = DeviceSeeds.DefaultDevice,
        ParameterId = ParameterSeeds.DefaultParameter.Id,
        Parameter = ParameterSeeds.DefaultParameter
    };
    
    public static readonly KpiEntity KpiToDelete =
        DefaultKpi with { Id = Guid.Parse("D359DB93-02BD-43EE-B894-464A80945B67") };
    
    public static readonly KpiEntity KpiToUpdate = DefaultKpi with { Id = Guid.Parse("B4EADEF2-FD03-4EAE-86D1-24447C165393") };
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KpiEntity>().HasData(
            DefaultKpi,
            KpiToDelete,
            KpiToUpdate
        );
    }
}