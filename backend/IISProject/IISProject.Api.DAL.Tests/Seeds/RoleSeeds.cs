using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.DAL.Tests.Seeds;

public static class RoleSeeds
{
    public static readonly RoleEntity DefaultRole = new()
    {
        Id = Guid.Parse("295C3085-D62A-4146-9BA9-6CCD20BFA78E"),
        Name = "Default"
    };
    
    public static readonly RoleEntity RoleToDelete =
        DefaultRole with { Id = Guid.Parse("9E500213-C736-430C-A6E3-B3F61B507639") };
    
    public static readonly RoleEntity RoleToUpdate = DefaultRole with { Id = Guid.Parse("C00FC0C2-5EF0-4B2A-AACC-28ED5CBA8243") };
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoleEntity>().HasData(
            DefaultRole with {Users = Array.Empty<UserEntity>()},
            RoleToDelete with {Users = Array.Empty<UserEntity>()},
            RoleToUpdate with {Users = Array.Empty<UserEntity>()}
        );
    }
}