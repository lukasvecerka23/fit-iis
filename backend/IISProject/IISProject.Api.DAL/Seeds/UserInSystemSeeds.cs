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
    
    public static readonly UserInSystemEntity DefaultUserInSystem2 = new()
    {
        Id = Guid.Parse("E85FEC0A-CD58-4BF8-BA22-F4267CD568CA"),
        UserId = UserSeeds.DefaultUser.Id,
        SystemId = SystemSeeds.DefaultSystem2.Id

    };
    
    public static readonly UserInSystemEntity DefaultUserInSystem3 = new()
    {
        Id = Guid.Parse("0806776F-5F6B-4880-90EA-CE99223DA085"),
        UserId = UserSeeds.DefaultUser.Id,
        SystemId = SystemSeeds.DefaultSystem3.Id

    };
    
    public static readonly UserInSystemEntity DefaultUserInSystem4 = new()
    {
        Id = Guid.Parse("5DFAABC0-36A5-43F3-BB4F-0D156765EDBB"),
        UserId = UserSeeds.DefaultUser2.Id,
        SystemId = SystemSeeds.DefaultSystem4.Id

    };

    public static IEnumerable<UserInSystemEntity> GetDefaultUserInSystems()
    {
        return new List<UserInSystemEntity>
        {
            DefaultUserInSystem,
            DefaultUserInSystem2,
            DefaultUserInSystem3,
            DefaultUserInSystem4
        };
    }
}