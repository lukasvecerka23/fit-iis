using IISProject.Api.Common.Seeds;
using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace IISProject.Api.DAL.Tests.Tests;

public class RoleOfUserRepositoryTests: DALTestsBase
{
    public RoleOfUserRepositoryTests(ITestOutputHelper output) : base(output){}
    
    [Fact]
    public void GetAll_RolesOfUsers()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<RoleOfUserEntity>();
        
        // act
        var rolesOfUsers = repository.GetAll();
        
        // assert
        Assert.True(rolesOfUsers.Contains(RoleOfUserSeeds.DefaultRoleOfUser));
        Assert.True(rolesOfUsers.Contains(RoleOfUserSeeds.RoleOfUserToDelete));
        Assert.True(rolesOfUsers.Contains(RoleOfUserSeeds.RoleOfUserToUpdate));
    }
    
    [Fact]
    public async Task InsertNew_RoleOfUser()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<RoleOfUserEntity>();
        var roleOfUser = new RoleOfUserEntity
        {
            Id = Guid.NewGuid(),
            RoleId = RoleSeeds.DefaultRole.Id,
            UserId = UserSeeds.DefaultUser.Id
        };
        
        // act
        var insertedRoleOfUser = await repository.InsertAsync(roleOfUser);
        await UnitOfWork.CommitAsync();
        
        // assert
        var retrieved = await repository.GetAll().Where(i => i.Id == insertedRoleOfUser.Id).SingleAsync();
        Assert.Equal(insertedRoleOfUser, retrieved);
    }
    
    [Fact]
    public async Task Update_RoleOfUser()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<RoleOfUserEntity>();
        var roleOfUser = RoleOfUserSeeds.RoleOfUserToUpdate with { RoleId = RoleSeeds.RoleToUpdate.Id };
        
        // act
        var updatedRoleOfUser = await repository.UpdateAsync(roleOfUser);
        await UnitOfWork.CommitAsync();
        
        // assert
        var retrieved = await repository.GetAll().Where(i => i.Id == updatedRoleOfUser.Id).SingleAsync();
        Assert.Equal(updatedRoleOfUser, retrieved);
    }
    
    [Fact]
    public async Task Delete_RoleOfUser()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<RoleOfUserEntity>();
        var roleOfUser = RoleOfUserSeeds.RoleOfUserToDelete;
        
        // act
        await repository.DeleteAsync(roleOfUser.Id);
        await UnitOfWork.CommitAsync();
        
        // assert
        var retrieved = await repository.GetAll().Where(i => i.Id == roleOfUser.Id).SingleOrDefaultAsync();
        Assert.Null(retrieved);
    }
}