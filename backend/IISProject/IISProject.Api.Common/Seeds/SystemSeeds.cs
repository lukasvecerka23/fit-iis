using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.Common.Seeds;

public static class SystemSeeds
{
    public static readonly SystemEntity DefaultSystem = new()
    {
        Id = Guid.Parse("9BBFFF33-EDCE-45B2-A4C9-5EC0279ED808"),
        Name = "Default",
        Description = "Default system",
        CreatorId = UserSeeds.DefaultUser.Id
    };
    
    public static readonly SystemEntity SystemToDelete =
        DefaultSystem with { Id = Guid.Parse("6B67FC86-375C-42CB-B754-428CBBBED925") };
    
    public static readonly SystemEntity SystemToUpdate = DefaultSystem with { Id = Guid.Parse("E8801587-BE03-4601-89D7-49433AEBCA46") };
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SystemEntity>().HasData(
            DefaultSystem with { Devices = Array.Empty<DeviceEntity>(), UsersInSystem = Array.Empty<UserInSystemEntity>() },
            SystemToDelete with { Devices = Array.Empty<DeviceEntity>(), UsersInSystem = Array.Empty<UserInSystemEntity>() },
            SystemToUpdate with { Devices = Array.Empty<DeviceEntity>(), UsersInSystem = Array.Empty<UserInSystemEntity>() }
        );
    }
}