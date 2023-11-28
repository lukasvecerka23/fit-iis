using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.DAL.Seeds;

public static class RoleSeeds
{
    public static readonly RoleEntity AdminRole = new()
    {
        Id = Guid.Parse("33A48020-D060-4A2D-8DDC-63E9CFA46131"),
        Name = "Admin"
    };
    
    public static readonly RoleEntity UserRole = new()
    {
        Id = Guid.Parse("DCECCA2B-63BE-4B10-B533-183B28944CC9"),
        Name = "User"
    };
    
    public static readonly RoleEntity BrokerRole = new()
    {
        Id = Guid.Parse("3C5B9BBC-7365-409B-8B41-151155665F4B"),
        Name = "Broker"
    };
    
    public static IEnumerable<RoleEntity> GetDefaultRoles()
    {
        return new List<RoleEntity>
        {
            UserRole with {Users = new List<UserEntity>()},
            AdminRole with {Users = new List<UserEntity>()},
            BrokerRole with {Users = new List<UserEntity>()}
        };
    }
}