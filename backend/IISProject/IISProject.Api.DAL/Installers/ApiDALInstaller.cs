using IISProject.Api.DAL.Factories;
using IISProject.Api.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IISProject.Api.DAL.Installers;

public class ApiDALInstaller
{
    public void Install(IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection.AddSingleton<IDbContextFactory<IISProjectDbContext>>(provider => new IISProjectDbContextFactory(connectionString, true));
        serviceCollection.AddSingleton<IDbMigrator, SqlDbMigrator>();

        serviceCollection.Scan(selector =>
            selector.FromAssemblyOf<ApiDALInstaller>()
                .AddClasses(classes => classes.AssignableTo(typeof(IRepository<>)))
                .AsMatchingInterface()
                .WithScopedLifetime());
    }
}