using IISProject.Api.Common.Seeds;
using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace IISProject.Api.DAL.Tests.Tests;

public class MeasurementRepositoryTests: DALTestsBase
{
    public MeasurementRepositoryTests(ITestOutputHelper output) : base(output){}
    
    [Fact]
    public void GetAll_Measurements()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<MeasurementEntity>();
        
        // act
        var measurements = repository.GetAll();
        
        // assert
        Assert.True(measurements.Contains(MeasurementSeeds.DefaultMeasurement));
        Assert.True(measurements.Contains(MeasurementSeeds.MeasurementToDelete));
        Assert.True(measurements.Contains(MeasurementSeeds.MeasurementToUpdate));
    }
    
    [Fact]
    public async Task InsertNew_Measurement()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<MeasurementEntity>();
        var measurement = new MeasurementEntity
        {
            Id = Guid.NewGuid(),
            Value = 10.0,
            TimeStamp = DateTime.Parse("2021-10-10T10:10:10.0000000"),
            DeviceId = DeviceSeeds.DefaultDevice.Id,
            CreatorId = UserSeeds.DefaultUser.Id,
            ParameterId = ParameterSeeds.DefaultParameter.Id
        };
        
        // act
        var insertedMeasurement = await repository.InsertAsync(measurement);
        await UnitOfWork.CommitAsync();
        
        // assert
        var retrieved = await repository.GetAll().Where(i => i.Id == insertedMeasurement.Id).SingleAsync();
        Assert.Equal(insertedMeasurement, retrieved);
    }
    
    [Fact]
    public async Task Update_Measurement()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<MeasurementEntity>();
        var measurement = MeasurementSeeds.MeasurementToUpdate with { Value = 20.0 };
        
        // act
        var updatedMeasurement = await repository.UpdateAsync(measurement);
        await UnitOfWork.CommitAsync();
        
        // assert
        var retrieved = await repository.GetAll().Where(i => i.Id == updatedMeasurement.Id).SingleAsync();
        Assert.Equal(updatedMeasurement, retrieved);
    }
    
    [Fact]
    public async Task Delete_Measurement()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<MeasurementEntity>();
        var measurement = MeasurementSeeds.MeasurementToDelete;
        
        // act
        await repository.DeleteAsync(measurement.Id);
        await UnitOfWork.CommitAsync();
        
        // assert
        var retrieved = await repository.GetAll().Where(i => i.Id == measurement.Id).SingleOrDefaultAsync();
        Assert.Null(retrieved);
    }
}