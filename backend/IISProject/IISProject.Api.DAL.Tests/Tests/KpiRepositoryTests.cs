using IISProject.Api.DAL.Tests.Seeds;
using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace IISProject.Api.DAL.Tests.Tests;

public class KpiRepositoryTests: DALTestsBase
{
    public KpiRepositoryTests(ITestOutputHelper output) : base(output){}

    [Fact]
    public void GetAll_Kpis()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<KpiEntity>();
        
        // act
        var kpis = repository.GetAll();
        
        // assert
        Assert.True(kpis.Contains(KpiSeeds.DefaultKpi));
        Assert.True(kpis.Contains(KpiSeeds.KpiToDelete));
        Assert.True(kpis.Contains(KpiSeeds.KpiToUpdate));
    }
    
    [Fact]
    public async Task InsertNew_Kpi()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<KpiEntity>();
        var kpi = new KpiEntity
        {
            Id = Guid.NewGuid(),
            Function = "New Kpi",
            DeviceId = DeviceSeeds.DefaultDevice.Id,
            CreatorId = UserSeeds.DefaultUser.Id,
            ParameterId = ParameterSeeds.DefaultParameter.Id,
            Error = true
        };
        
        // act
        var insertedKpi = await repository.InsertAsync(kpi);
        await UnitOfWork.CommitAsync();
        
        // assert
        var retrieved = await repository.GetAll().Where(i => i.Id == insertedKpi.Id).SingleAsync();
        Assert.Equal(insertedKpi, retrieved);
    }
    
    [Fact]
    public async Task Update_Kpi()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<KpiEntity>();
        var kpi = KpiSeeds.KpiToUpdate with { Function = "Updated Kpi" };
        
        // act
        var updatedKpi = await repository.UpdateAsync(kpi);
        await UnitOfWork.CommitAsync();
        
        // assert
        var retrieved = await repository.GetAll().Where(i => i.Id == updatedKpi.Id).SingleAsync();
        Assert.Equal(updatedKpi, retrieved);
    }
    
    [Fact]
    public async Task Delete_Kpi()
    {
        // arrange
        var repository = UnitOfWork.GetRepository<KpiEntity>();
        var kpi = KpiSeeds.KpiToDelete;
        
        // act
        await repository.DeleteAsync(kpi.Id);
        await UnitOfWork.CommitAsync();
        
        // assert
        var retrieved = await repository.GetAll().Where(i => i.Id == kpi.Id).SingleOrDefaultAsync();
        Assert.Null(retrieved);
    }
}