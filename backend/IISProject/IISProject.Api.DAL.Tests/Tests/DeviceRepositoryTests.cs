using IISProject.Api.DAL.Seeds;
using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;
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

    [Fact]
    public async Task InsertNew_Device()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<DeviceEntity>();
        var device = new DeviceEntity
        {
            Id = Guid.NewGuid(),
            UserAlias = "New Device",
            DeviceTypeId = DeviceTypeSeeds.DefaultDeviceType.Id,
            CreatorId = UserSeeds.DefaultUser.Id,
            SystemId = SystemSeeds.DefaultSystem.Id,
            Description = "New Device Description"
        };
        
        // act
        var insertedDevice = await repository.InsertAsync(device);
        await UnitOfWork.CommitAsync();
        
        // assert
        var retrieved = await repository.GetAll().Where(i => i.Id == insertedDevice.Id).SingleAsync();
        Assert.Equal(insertedDevice, retrieved);
    }
    
    [Fact]
    public async Task Update_Device()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<DeviceEntity>();
        var device = DeviceSeeds.DeviceToUpdate with { UserAlias = "Updated Device" };
        
        // act
        var updatedDevice = await repository.UpdateAsync(device);
        await UnitOfWork.CommitAsync();
        
        // assert
        var retrieved = await repository.GetAll().Where(i => i.Id == updatedDevice.Id).SingleAsync();
        Assert.Equal(updatedDevice, retrieved);
    }
    
    [Fact]
    public async Task Delete_Device()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<DeviceEntity>();
        var device = DeviceSeeds.DeviceToDelete;
        
        // act
        await repository.DeleteAsync(device.Id);
        await UnitOfWork.CommitAsync();
        
        // assert
        var retrieved = await repository.GetAll().Where(i => i.Id == device.Id).SingleOrDefaultAsync();
        Assert.Null(retrieved);
    }
}