using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.DAL.Seeds;

public static class DeviceTypeSeeds
{
    public static readonly DeviceTypeEntity DefaultDeviceType = new()
    {
        Id = Guid.Parse("C7BEC5C9-736D-4CD3-A75E-BB2F45FBCE51"),
        Name = "Senzor teploty",
    };
    
    public static IEnumerable<DeviceTypeEntity> GetDefaultDeviceTypes()
    {
        return new List<DeviceTypeEntity>
        {
            DefaultDeviceType with
            {
                Devices = new List<DeviceEntity>(), Parameters = new List<ParameterEntity>()
            }
        };
    }
}