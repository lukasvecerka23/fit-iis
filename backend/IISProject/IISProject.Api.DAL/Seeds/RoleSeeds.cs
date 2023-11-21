using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.DAL.Seeds;

public static class RoleSeeds
{
    public static readonly RoleEntity DefaultRole = new()
    {
        Id = Guid.Parse("295C3085-D62A-4146-9BA9-6CCD20BFA78E"),
        Name = "Default"
    };
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoleEntity>().HasData(
            DefaultRole with { RoleOfUsers = Array.Empty<RoleOfUserEntity>() }
        );
    }
}