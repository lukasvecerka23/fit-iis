using IISProject.Api.Common.Seeds;
using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace IISProject.Api.DAL.Tests.Tests;

public class UserInSystemRepositoryTests: DALTestsBase
{
    public UserInSystemRepositoryTests(ITestOutputHelper output) : base(output){}
    
    [Fact]
    public void GetAll_UsersInSystems()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<UserInSystemEntity>();
        
        // act
        var usersInSystems = repository.GetAll();
        
        // assert
        Assert.True(usersInSystems.Contains(UserInSystemSeeds.DefaultUserInSystem));
        Assert.True(usersInSystems.Contains(UserInSystemSeeds.UserInSystemToDelete));
        Assert.True(usersInSystems.Contains(UserInSystemSeeds.UserInSystemToUpdate));
    }
    
    [Fact]
    public async Task InsertNew_UserInSystem()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<UserInSystemEntity>();
        var userInSystem = new UserInSystemEntity
        {
            Id = Guid.NewGuid(),
            SystemId = SystemSeeds.DefaultSystem.Id,
            UserId = UserSeeds.DefaultUser.Id
        };
        
        // act
        var insertedUserInSystem = await repository.InsertAsync(userInSystem);
        await UnitOfWork.CommitAsync();
        
        // assert
        var retrieved = await repository.GetAll().Where(i => i.Id == insertedUserInSystem.Id).SingleAsync();
        Assert.Equal(insertedUserInSystem, retrieved);
    }
    
    [Fact]
    public async Task Update_UserInSystem()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<UserInSystemEntity>();
        var userInSystem = UserInSystemSeeds.UserInSystemToUpdate with { SystemId = SystemSeeds.SystemToUpdate.Id };
        
        // act
        var updatedUserInSystem = await repository.UpdateAsync(userInSystem);
        await UnitOfWork.CommitAsync();
        
        // assert
        var retrieved = await repository.GetAll().Where(i => i.Id == updatedUserInSystem.Id).SingleAsync();
        Assert.Equal(updatedUserInSystem, retrieved);
    }
    
    [Fact]
    public async Task Delete_UserInSystem()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<UserInSystemEntity>();
        var userInSystem = UserInSystemSeeds.UserInSystemToDelete;
        
        // act
        await repository.DeleteAsync(userInSystem.Id);
        await UnitOfWork.CommitAsync();
        
        // assert
        var retrieved = await repository.GetAll().Where(i => i.Id == userInSystem.Id).SingleOrDefaultAsync();
        Assert.Null(retrieved);
    }
}