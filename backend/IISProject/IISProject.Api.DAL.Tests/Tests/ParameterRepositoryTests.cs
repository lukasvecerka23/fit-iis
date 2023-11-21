using IISProject.Api.DAL.Tests.Seeds;
using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace IISProject.Api.DAL.Tests.Tests;

public class ParameterRepositoryTests: DALTestsBase
{
    public ParameterRepositoryTests(ITestOutputHelper output) : base(output){}
    
    [Fact]
    public void GetAll_Parameters()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<ParameterEntity>();
        
        // act
        var parameters = repository.GetAll();
        
        // assert
        Assert.True(parameters.Contains(ParameterSeeds.DefaultParameter));
        Assert.True(parameters.Contains(ParameterSeeds.ParameterToDelete));
        Assert.True(parameters.Contains(ParameterSeeds.ParameterToUpdate));
    }
    
    [Fact]
    public async Task InsertNew_Parameter()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<ParameterEntity>();
        var parameter = new ParameterEntity
        {
            Id = Guid.NewGuid(),
            Name = "New Parameter",
            LowerLimit = -10.0,
            UpperLimit = 10.0,
            DeviceTypeId = DeviceTypeSeeds.DefaultDeviceType.Id
        };
        
        // act
        var insertedParameter = await repository.InsertAsync(parameter);
        await UnitOfWork.CommitAsync();
        
        // assert
        var retrieved = await repository.GetAll().Where(i => i.Id == insertedParameter.Id).SingleAsync();
        Assert.Equal(insertedParameter, retrieved);
    }
    
    [Fact]
    public async Task Update_Parameter()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<ParameterEntity>();
        var parameter = ParameterSeeds.ParameterToUpdate with { Name = "Updated Parameter" };
        
        // act
        var updatedParameter = await repository.UpdateAsync(parameter);
        await UnitOfWork.CommitAsync();
        
        // assert
        var retrieved = await repository.GetAll().Where(i => i.Id == updatedParameter.Id).SingleAsync();
        Assert.Equal(updatedParameter, retrieved);
    }
    
    [Fact]
    public async Task Delete_Parameter()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<ParameterEntity>();
        var parameter = ParameterSeeds.ParameterToDelete;
        
        // act
        await repository.DeleteAsync(parameter.Id);
        await UnitOfWork.CommitAsync();
        
        // assert
        var retrieved = await repository.GetAll().Where(i => i.Id == parameter.Id).SingleOrDefaultAsync();
        Assert.Null(retrieved);
    }
}