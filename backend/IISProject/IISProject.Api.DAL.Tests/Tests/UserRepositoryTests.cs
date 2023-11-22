using IISProject.Api.DAL.Tests.Seeds;
using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace IISProject.Api.DAL.Tests.Tests;

public class UserRepositoryTests: DALTestsBase
{
    public UserRepositoryTests(ITestOutputHelper output) : base(output){}
    
    [Fact]
    public void GetAll_Users()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<UserEntity>();
        
        // act
        var users = repository.GetAll();
        
        // assert
        Assert.True(users.Contains(UserSeeds.DefaultUser));
        Assert.True(users.Contains(UserSeeds.UserToDelete));
        Assert.True(users.Contains(UserSeeds.UserToUpdate));
    }

    [Fact]
    public async Task InsertNew_User()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<UserEntity>();
        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Name = "Poe",
            Surname = "Poe",
            Username = "poepoe",
            PasswordHash = "TESTHASH"
        };

        // act
        var insertedUser = await repository.InsertAsync(user);
        await UnitOfWork.CommitAsync();

        // assert
        var retrieved = await repository.GetAll().Where(i => i.Id == insertedUser.Id).SingleAsync();
        Assert.Equal(insertedUser, retrieved);
    }
    
    [Fact]
    public async Task Update_User()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<UserEntity>();
        var user = UserSeeds.UserToUpdate with { Name = "Updated User" };
        
        // act
        var updatedUser = await repository.UpdateAsync(user);
        await UnitOfWork.CommitAsync();
        
        // assert
        var retrieved = await repository.GetAll().Where(i => i.Id == updatedUser.Id).SingleAsync();
        Assert.Equal(updatedUser, retrieved);
    }
    
    [Fact]
    public async Task Delete_User()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<UserEntity>();
        var user = UserSeeds.UserToDelete;
        
        // act
        await repository.DeleteAsync(user.Id);
        await UnitOfWork.CommitAsync();
        
        // assert
        var retrieved = await repository.GetAll().Where(i => i.Id == user.Id).SingleOrDefaultAsync();
        Assert.Null(retrieved);
    }
}