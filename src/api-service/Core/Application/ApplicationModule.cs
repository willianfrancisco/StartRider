using Application.Ports;
using Application.UserCases;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationModule
{
    public static IServiceCollection AdicionaDependenciasApplications(this IServiceCollection service)
    {
        service.AddScoped<IVeiculoUseCase,VeiculoUseCase>();
        service.AddScoped<ILocatarioUseCase, LocatarioUseCase>();
        service.AddScoped<ILocacaoUseCase, LocacaoUseCase>();
        service.AddTransient<ITokenUseCase, TokenUseCase>();
        service.AddTransient<IUsuarioUseCase, UsuarioUseCase>();
        return service;
    }
}