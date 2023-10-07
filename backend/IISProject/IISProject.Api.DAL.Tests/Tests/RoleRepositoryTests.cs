using IISProject.Api.Common.Seeds;
using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace IISProject.Api.DAL.Tests.Tests;

public class RoleRepositoryTests: DALTestsBase
{
    public RoleRepositoryTests(ITestOutputHelper output) : base(output){}
    
    [Fact]
    public void GetAll_Roles()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<RoleEntity>();
        
        // act
        var roles = repository.GetAll();
        
        // assert
        Assert.True(roles.Contains(RoleSeeds.DefaultRole));
        Assert.True(roles.Contains(RoleSeeds.RoleToDelete));
        Assert.True(roles.Contains(RoleSeeds.RoleToUpdate));
    }
    
    [Fact]
    public async Task InsertNew_Role()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<RoleEntity>();
        var role = new RoleEntity
        {
            Id = Guid.NewGuid(),
            Name = "New Role"
        };
        
        // act
        var insertedRole = await repository.InsertAsync(role);
        await UnitOfWork.CommitAsync();
        
        // assert
        var retrieved = await repository.GetAll().Where(i => i.Id == insertedRole.Id).SingleAsync();
        Assert.Equal(insertedRole, retrieved);
    }
    
    [Fact]
    public async Task Update_Role()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<RoleEntity>();
        var role = RoleSeeds.RoleToUpdate with { Name = "Updated Role" };
        
        // act
        var updatedRole = await repository.UpdateAsync(role);
        await UnitOfWork.CommitAsync();
        
        // assert
        var retrieved = await repository.GetAll().Where(i => i.Id == updatedRole.Id).SingleAsync();
        Assert.Equal(updatedRole, retrieved);
    }
    
    [Fact]
    public async Task Delete_Role()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<RoleEntity>();
        var role = RoleSeeds.RoleToDelete;
        
        // act
        await repository.DeleteAsync(role.Id);
        await UnitOfWork.CommitAsync();
        
        // assert
        var retrieved = await repository.GetAll().Where(i => i.Id == role.Id).SingleOrDefaultAsync();
        Assert.Null(retrieved);
    }
}