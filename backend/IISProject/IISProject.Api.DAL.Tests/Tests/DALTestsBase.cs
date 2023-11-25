using AutoMapper;
using IISProject.Api.DAL.Entities;
using IISProject.Api.DAL.Tests.Factories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace IISProject.Api.DAL.Tests;

public class DALTestsBase : IAsyncLifetime
{
    protected IDbContextFactory<TestingDbContext> DbContextFactory { get; }
    protected TestingDbContext DbContext { get; }
    protected UnitOfWork.UnitOfWork UnitOfWork { get; }
    
    protected DALTestsBase(ITestOutputHelper output)
    {
        DbContextFactory = new DbContextTestingFactory(GetType().FullName!, true);
        DbContext = DbContextFactory.CreateDbContext();

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<DeviceEntity.DeviceEntityProfile>();
            cfg.AddProfile<DeviceTypeEntity.DeviceTypeEntityProfile>();
            cfg.AddProfile<KpiEntity.KpiEntityProfile>();
            cfg.AddProfile<MeasurementEntity.MeasurementEntityProfile>();
            cfg.AddProfile<ParameterEntity.ParameterEntityProfile>();
            cfg.AddProfile<RoleEntity.RoleEntityProfile>();
            cfg.AddProfile<SystemEntity.SystemEntityProfile>();
            cfg.AddProfile<UserEntity.UserEntityProfile>();
            cfg.AddProfile<UserInSystemEntity.UserInSystemEntityProfile>();
        });

        var mapper = mapperConfig.CreateMapper();
        UnitOfWork = new UnitOfWork.UnitOfWork(DbContext, mapper);
    }
    
    public async Task InitializeAsync()
    {
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.Database.EnsureCreatedAsync();
    }
    
    public async Task DisposeAsync()
    {
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.DisposeAsync();
    }
}