using IISProject.Api.DAL.Tests.Seeds;
using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace IISProject.Api.DAL.Tests.Tests;

public class DeviceTypeRepositoryTests: DALTestsBase
{
    public DeviceTypeRepositoryTests(ITestOutputHelper output) : base(output){}
    
    [Fact]
    public void GetAll_DeviceTypes()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<DeviceTypeEntity>();
        
        // act
        var deviceTypes = repository.GetAll();
        
        // assert
        Assert.True(deviceTypes.Contains(DeviceTypeSeeds.DefaultDeviceType));
        Assert.True(deviceTypes.Contains(DeviceTypeSeeds.DeviceTypeToDelete));
        Assert.True(deviceTypes.Contains(DeviceTypeSeeds.DeviceTypeToUpdate));
    }
    
    [Fact]
    public async Task InsertNew_DeviceType()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<DeviceTypeEntity>();
        var deviceType = new DeviceTypeEntity
        {
            Id = Guid.NewGuid(),
            Name = "New Device Type"
        };
        
        // act
        var insertedDeviceType = await repository.InsertAsync(deviceType);
        await UnitOfWork.CommitAsync();
        
        // assert
        var retrieved = await repository.GetAll().Where(i => i.Id == insertedDeviceType.Id).SingleAsync();
        Assert.Equal(insertedDeviceType, retrieved);
    }
    
    [Fact]
    public async Task Update_DeviceType()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<DeviceTypeEntity>();
        var deviceType = DeviceTypeSeeds.DeviceTypeToUpdate with { Name = "Updated Device Type" };
        
        // act
        var updatedDeviceType = await repository.UpdateAsync(deviceType);
        await UnitOfWork.CommitAsync();
        
        // assert
        var retrieved = await repository.GetAll().Where(i => i.Id == updatedDeviceType.Id).SingleAsync();
        Assert.Equal(updatedDeviceType, retrieved);
    }
    
    [Fact]
    public async Task Delete_DeviceType()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<DeviceTypeEntity>();
        var deviceType = DeviceTypeSeeds.DeviceTypeToDelete;
        
        // act
        await repository.DeleteAsync(deviceType.Id);
        await UnitOfWork.CommitAsync();
        
        // assert
        var retrieved = await repository.GetAll().Where(i => i.Id == deviceType.Id).SingleOrDefaultAsync();
        Assert.Null(retrieved);
    }
}