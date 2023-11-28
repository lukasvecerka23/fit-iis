using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.DAL.Seeds;

public static class SystemSeeds
{
    public static readonly SystemEntity DefaultSystem = new()
    {
        Id = Guid.Parse("9BBFFF33-EDCE-45B2-A4C9-5EC0279ED808"),
        Name = "Kuchyň",
        Description = "Default system",
        CreatorId = UserSeeds.DefaultUser.Id
    };
    
    public static readonly SystemEntity DefaultSystem2 = new()
    {
        Id = Guid.Parse("192667A5-B0F4-4CDA-A0C3-6C85141E4136"),
        Name = "Dvůr",
        Description = "Default system 2",
        CreatorId = UserSeeds.DefaultUser.Id
    };
    
    public static readonly SystemEntity DefaultSystem3 = new()
    {
        Id = Guid.Parse("727CC83A-DE45-4011-A777-ECFC702B6AFC"),
        Name = "Sklep",
        Description = "Default system 3",
        CreatorId = UserSeeds.DefaultUser.Id
    };
    
    public static readonly SystemEntity DefaultSystem4 = new()
    {
        Id = Guid.Parse("018BF066-23C6-4B94-916E-95CF3DAA6539"),
        Name = "Zahrada",
        Description = "Default system 3",
        CreatorId = UserSeeds.DefaultUser2.Id
    };
    
    public static IEnumerable<SystemEntity> GetDefaultSystems()
    {
        return new List<SystemEntity>
        {
            DefaultSystem with { Devices = new List<DeviceEntity>(), UsersInSystem = new List<UserInSystemEntity>(), AssignsToSystems = new List<AssignToSystemEntity>() },
            DefaultSystem2 with { Devices = new List<DeviceEntity>(), UsersInSystem = new List<UserInSystemEntity>(), AssignsToSystems = new List<AssignToSystemEntity>() },
            DefaultSystem3 with { Devices = new List<DeviceEntity>(), UsersInSystem = new List<UserInSystemEntity>(), AssignsToSystems = new List<AssignToSystemEntity>() },
            DefaultSystem4 with { Devices = new List<DeviceEntity>(), UsersInSystem = new List<UserInSystemEntity>(), AssignsToSystems = new List<AssignToSystemEntity>() }
        };
    }
}