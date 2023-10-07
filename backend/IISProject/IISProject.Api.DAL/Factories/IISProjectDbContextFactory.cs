using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.DAL.Factories;

public class IISProjectDbContextFactory: IDbContextFactory<IISProjectDbContext>
{
    private readonly string _connectionString;
    private readonly bool _seedData;

    public IISProjectDbContextFactory(string connectionString, bool seedData)
    {
        _connectionString = connectionString;
        _seedData = seedData;
    }
    
    public IISProjectDbContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<IISProjectDbContext>();
        optionsBuilder.UseSqlServer(_connectionString);

        IISProjectDbContext dbContext = new(optionsBuilder.Options, _seedData);

        return dbContext;
    }
}