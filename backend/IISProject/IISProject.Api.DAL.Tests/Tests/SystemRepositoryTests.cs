using IISProject.Api.DAL.Tests.Seeds;
using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace IISProject.Api.DAL.Tests.Tests;

public class SystemRepositoryTests: DALTestsBase
{
    public SystemRepositoryTests(ITestOutputHelper output) : base(output){}
    
    [Fact]
    public void GetAll_Systems()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<SystemEntity>();
        
        // act
        var systems = repository.GetAll();
        
        // assert
        Assert.True(systems.Contains(SystemSeeds.DefaultSystem));
        Assert.True(systems.Contains(SystemSeeds.SystemToDelete));
        Assert.True(systems.Contains(SystemSeeds.SystemToUpdate));
    }
    
    [Fact]
    public async Task InsertNew_System()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<SystemEntity>();
        var system = new SystemEntity
        {
            Id = Guid.NewGuid(),
            Name = "New System",
            CreatorId = UserSeeds.DefaultUser.Id
        };
        
        // act
        var insertedSystem = await repository.InsertAsync(system);
        await UnitOfWork.CommitAsync();
        
        // assert
        var retrieved = await repository.GetAll().Where(i => i.Id == insertedSystem.Id).SingleAsync();
        Assert.Equal(insertedSystem, retrieved);
    }
    
    [Fact]
    public async Task Update_System()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<SystemEntity>();
        var system = SystemSeeds.SystemToUpdate with { Name = "Updated System" };
        
        // act
        var updatedSystem = await repository.UpdateAsync(system);
        await UnitOfWork.CommitAsync();
        
        // assert
        var retrieved = await repository.GetAll().Where(i => i.Id == updatedSystem.Id).SingleAsync();
        Assert.Equal(updatedSystem, retrieved);
    }
    
    [Fact]
    public async Task Delete_System()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<SystemEntity>();
        var system = SystemSeeds.SystemToDelete;
        
        // act
        await repository.DeleteAsync(system.Id);
        await UnitOfWork.CommitAsync();
        
        // assert
        var retrieved = await repository.GetAll().Where(i => i.Id == system.Id).SingleOrDefaultAsync();
        Assert.Null(retrieved);
    }
}