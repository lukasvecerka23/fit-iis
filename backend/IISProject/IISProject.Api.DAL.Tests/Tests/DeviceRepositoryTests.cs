using IISProject.Api.Common.Seeds;
using IISProject.Api.DAL.Entities;
using Xunit;
using Xunit.Abstractions;

namespace IISProject.Api.DAL.Tests.Tests;

public class DeviceRepositoryTests: DALTestsBase
{
    public DeviceRepositoryTests(ITestOutputHelper output) : base(output){}

    [Fact]
    public void GetAll_Devices()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<DeviceEntity>();
        
        // act
        var devices = repository.GetAll();
        
        // assert
        Assert.True(devices.Contains(DeviceSeeds.DefaultDevice));
        Assert.True(devices.Contains(DeviceSeeds.DeviceToDelete));
        Assert.True(devices.Contains(DeviceSeeds.DeviceToUpdate));
    }
}