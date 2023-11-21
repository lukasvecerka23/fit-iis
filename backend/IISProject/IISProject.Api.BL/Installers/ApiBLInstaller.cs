using IISProject.Api.BL.Facades.Interfaces;
using IISProject.Api.Common.Installers;
using IISProject.Api.DAL.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace IISProject.Api.BL.Installers;

public class ApiBLInstaller: IInstaller
{
    public void Install(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();
        
        serviceCollection.Scan(selector =>
            selector.FromAssemblyOf<ApiBLInstaller>()
                .AddClasses(classes => classes.AssignableTo(typeof(IFacade<,,,>)))
                .AsSelfWithInterfaces()
                .WithScopedLifetime());
    }
}