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
    public DbSet<AssignToSystemEntity> AssignsToSystems => Set<AssignToSystemEntity>();
    
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
            
            entity.HasMany(i => i.AssignsToSystems)
                .WithOne(i => i.System)
                .OnDelete(DeleteBehavior.Cascade);
            
        });
        
        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasMany(i => i.Kpis)
                .WithOne(i => i.Creator)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasMany(i => i.UserInSystems)
                .WithOne(i => i.User)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(i => i.AssignsToSystems)
                .WithOne(i => i.User)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    public async Task SeedDatabaseAsync()
    {
        if (!this.Roles.Any())
        {
            // Add seed data for Roles
            this.Roles.AddRange(RoleSeeds.GetDefaultRoles());
            await this.SaveChangesAsync();
        }
        
        if (!this.Users.Any())
        {
            // Add seed data for Users
            this.Users.AddRange(UserSeeds.GetDefaultUsers());
            await this.SaveChangesAsync();
        }
        
        if (!this.Systems.Any())
        {
            // Add seed data for Systems
            this.Systems.AddRange(SystemSeeds.GetDefaultSystems());
            await this.SaveChangesAsync();
        }
        
        if (!this.UserInSystems.Any())
        {
            // Add seed data for UserInSystems
            this.UserInSystems.AddRange(UserInSystemSeeds.GetDefaultUserInSystems());
            await this.SaveChangesAsync();
        }
        
        if (!this.AssignsToSystems.Any())
        {
            // Add seed data for AssignsToSystems
            this.AssignsToSystems.AddRange(AssignToSystemSeeds.GetDefaultAssignsToSystems());
            await this.SaveChangesAsync();
        }
        
        if (!this.DeviceTypes.Any())
        {
            // Add seed data for DeviceTypes
            this.DeviceTypes.AddRange(DeviceTypeSeeds.GetDefaultDeviceTypes());
            await this.SaveChangesAsync();
        }
        
        if (!this.Devices.Any())
        {
            // Add seed data for Devices
            this.Devices.AddRange(DeviceSeeds.GetDefaultDevices());
            await this.SaveChangesAsync();
        }
        
        if (!this.Parameters.Any())
        {
            // Add seed data for Parameters
            this.Parameters.AddRange(ParameterSeeds.GetDefaultParameters());
            await this.SaveChangesAsync();
        }
        
        if (!this.Kpis.Any())
        {
            // Add seed data for Kpis
            this.Kpis.AddRange(KpiSeeds.GetDefaultKpis());
            await this.SaveChangesAsync();
        }
        
        if (!this.Measurements.Any())
        {
            // Add seed data for Measurements
            this.Measurements.AddRange(MeasurementSeeds.GetDefaultMeasurements());
            await this.SaveChangesAsync();
        }
    }
}