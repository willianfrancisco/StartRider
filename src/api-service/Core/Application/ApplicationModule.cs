using Application.Ports;
using Application.UserCases;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationModule
{
    public static IServiceCollection AdicionaDependenciasApplications(this IServiceCollection service)
    {
        service.AddScoped<IVeiculoUseCase,VeiculoUseCase>();
        return service;
    }
}