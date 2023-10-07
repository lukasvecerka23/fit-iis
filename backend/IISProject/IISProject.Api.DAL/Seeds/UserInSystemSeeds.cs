using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.DAL.Seeds;

public static class UserInSystemSeeds
{
    public static readonly UserInSystemEntity DefaultUserInSystem = new()
    {
        Id = Guid.Parse("E44E156A-9C07-4288-B222-ABED81FA7989"),
        UserId = UserSeeds.DefaultUser.Id,
        SystemId = SystemSeeds.DefaultSystem.Id

    };
    
    public static readonly UserInSystemEntity UserInSystemToDelete =
        DefaultUserInSystem with { Id = Guid.Parse("7049E4FB-CF2B-4FCA-B1D2-8F347DC7702E") };
    
    public static readonly UserInSystemEntity UserInSystemToUpdate = DefaultUserInSystem with { Id = Guid.Parse("5593EB75-3B44-4582-920A-C60743FB170B") };
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserInSystemEntity>().HasData(
            DefaultUserInSystem,
            UserInSystemToDelete,
            UserInSystemToUpdate
        );
    }

}