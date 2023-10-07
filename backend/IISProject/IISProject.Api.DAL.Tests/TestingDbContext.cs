using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.DAL.Tests;

public class TestingDbContext: IISProjectDbContext
{
    private readonly bool _seedTestingData;
    
    public TestingDbContext(DbContextOptions<IISProjectDbContext> options, bool seedTestingData = false)
    : base(options, seedDemoData: false)
    {
        _seedTestingData = seedTestingData;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        if (_seedTestingData)
        {
            // TODO: Seed testing data
        }
    }
}