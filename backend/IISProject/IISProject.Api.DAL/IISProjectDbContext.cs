using IISProject.Api.DAL.Seeds;
using IISProject.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.DAL;

public class IISProjectDbContext : DbContext
{
    private readonly bool _seedDemoData;

    public IISProjectDbContext(DbContextOptions contextOptions, bool seedDemoData = false)
        : base(contextOptions)
    {
        _seedDemoData = seedDemoData;       
    }
    
    public DbSet<DeviceEntity> Devices => Set<DeviceEntity>();
    public DbSet<DeviceTypeEntity> DeviceTypes => Set<DeviceTypeEntity>();
    public DbSet<KpiEntity> Kpis => Set<KpiEntity>();
    public DbSet<MeasurementEntity> Measurements => Set<MeasurementEntity>();
    public DbSet<ParameterEntity> Parameters => Set<ParameterEntity>();
    public DbSet<RoleEntity> Roles => Set<RoleEntity>();
    public DbSet<SystemEntity> Systems => Set<SystemEntity>();
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<UserInSystemEntity> UserInSystems => Set<UserInSystemEntity>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DeviceEntity>(entity =>
        {
            entity.HasMany(i => i.Measurements)
                .WithOne(i => i.Device)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(i => i.Kpis)
                .WithOne(i => i.Device)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<DeviceTypeEntity>(entity =>
        {
            entity.HasMany(i => i.Parameters)
                .WithOne(i => i.DeviceType)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(i => i.Devices)
                .WithOne(i => i.DeviceType)
                .OnDelete(DeleteBehavior.SetNull);
            
        });

        modelBuilder.Entity<ParameterEntity>(entity =>
        {
            entity.HasMany(i => i.Measurements)
                .WithOne(i => i.Parameter)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(i => i.Kpis)
                .WithOne(i => i.Parameter)
                .OnDelete(DeleteBehavior.Cascade);

        });
        
        modelBuilder.Entity<RoleEntity>(entity =>
        {
            entity.HasMany(i => i.Users)
                .WithOne(i => i.Role)
                .OnDelete(DeleteBehavior.Cascade);

        });
        
        modelBuilder.Entity<SystemEntity>(entity =>
        {
            entity.HasMany(i => i.UsersInSystem)
                .WithOne(i => i.System)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(i => i.Devices)
                .WithOne(i => i.System)
                .OnDelete(DeleteBehavior.SetNull);
            
        });
        
        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasMany(i => i.Kpis)
                .WithOne(i => i.Creator)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasMany(i => i.UserInSystems)
                .WithOne(i => i.User)
                .OnDelete(DeleteBehavior.Cascade);
        });

        if (_seedDemoData)
        {
            DeviceSeeds.Seed(modelBuilder);
            DeviceTypeSeeds.Seed(modelBuilder);
            KpiSeeds.Seed(modelBuilder);
            MeasurementSeeds.Seed(modelBuilder);
            ParameterSeeds.Seed(modelBuilder);
            RoleSeeds.Seed(modelBuilder);
            SystemSeeds.Seed(modelBuilder);
            UserSeeds.Seed(modelBuilder);
            UserInSystemSeeds.Seed(modelBuilder);
        }
        
    }
}