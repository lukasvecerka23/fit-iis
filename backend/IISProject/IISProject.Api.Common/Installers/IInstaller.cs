using Microsoft.Extensions.DependencyInjection;

namespace IISProject.Api.Common.Installers;

public interface IInstaller
{
    void Install(IServiceCollection serviceCollection);
}