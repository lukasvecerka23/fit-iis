using Microsoft.EntityFrameworkCore;

namespace IISProject.Api.DAL.Installers;

public interface IDbMigrator
{
    public void Migrate();
    public Task MigrateAsync(CancellationToken cancellationToken);
}

public class SqlDbMigrator: IDbMigrator
{
    private readonly IDbContextFactory<IISProjectDbContext> _dbContextFactory;

    public SqlDbMigrator(IDbContextFactory<IISProjectDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public void Migrate() => MigrateAsync(CancellationToken.None).GetAwaiter().GetResult();

    public async Task MigrateAsync(CancellationToken cancellationToken)
    {
        await using IISProjectDbContext dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        // If you want to delete the database before migration, uncomment the following line
        await dbContext.Database.MigrateAsync(cancellationToken);

        await dbContext.SeedDatabaseAsync();
    }
}