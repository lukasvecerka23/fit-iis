using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.DAL.Tests.Factories;

public class DbContextTestingFactory : IDbContextFactory<TestingDbContext>
{
    private readonly string _databaseName;
    private readonly bool _seedTestingData;

    public DbContextTestingFactory(string databaseName, bool seedTestingData = false)
    {
        _databaseName = databaseName;
        _seedTestingData = seedTestingData;
    }

    public TestingDbContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<IISProjectDbContext>();
        optionsBuilder.UseInMemoryDatabase(_databaseName);

        return new TestingDbContext(optionsBuilder.Options, _seedTestingData);
    }
}