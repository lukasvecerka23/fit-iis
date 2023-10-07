using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.Common.Seeds;

public static class RoleOfUserSeeds
{
    public static readonly RoleOfUserEntity DefaultRoleOfUser = new()
    {
        Id = Guid.Parse("A80FE169-3BBC-4B4F-B25E-93339A46B03F"),
        RoleId = RoleSeeds.DefaultRole.Id,
        UserId = UserSeeds.DefaultUser.Id,
        User = UserSeeds.DefaultUser,
        Role = RoleSeeds.DefaultRole
    };
    
    public static readonly RoleOfUserEntity RoleOfUserToDelete =
        DefaultRoleOfUser with { Id = Guid.Parse("E454F601-3ED8-4F38-834B-875D6823D6A8") };
    
    public static readonly RoleOfUserEntity RoleOfUserToUpdate = DefaultRoleOfUser with { Id = Guid.Parse("E39E03A0-BFE2-41EA-84A1-7D3E5B42264C") };
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoleOfUserEntity>().HasData(
            DefaultRoleOfUser,
            RoleOfUserToDelete,
            RoleOfUserToUpdate
        );
    }
}