using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace IISProject.Api.DAL.Factories;

public class IISProjectDbContextFactory: IDesignTimeDbContextFactory<IISProjectDbContext>
{
    public IISProjectDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<IISProjectDbContextFactory>(optional: true)
            .Build();
        
        var optionsBuilder = new DbContextOptionsBuilder<IISProjectDbContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        return new IISProjectDbContext(optionsBuilder.Options);
    }
    
}