using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.DAL.Seeds;

public static class AssignToSystemSeeds
{
    public static readonly AssignToSystemEntity DefaultAssign = new()
    {
        Id = Guid.Parse("152265C1-A3A5-45D5-A0B8-87DD06AE5097"),
        UserId = UserSeeds.DefaultUser2.Id,
        SystemId = SystemSeeds.DefaultSystem.Id
    };
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssignToSystemEntity>().HasData(
            DefaultAssign
        );
    }
}