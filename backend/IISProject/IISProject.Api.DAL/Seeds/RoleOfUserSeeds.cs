using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.DAL.Seeds;

public static class RoleOfUserSeeds
{
    public static readonly RoleOfUserEntity DefaultRoleOfUser = new()
    {
        Id = Guid.Parse("A80FE169-3BBC-4B4F-B25E-93339A46B03F"),
        RoleId = RoleSeeds.DefaultRole.Id,
        UserId = UserSeeds.DefaultUser.Id
    };
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoleOfUserEntity>().HasData(
            DefaultRoleOfUser
        );
    }
}