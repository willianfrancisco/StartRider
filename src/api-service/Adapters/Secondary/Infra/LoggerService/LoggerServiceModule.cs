using Domain.Ports;
using LoggerService.Serilog;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace LoggerService;

public static class LoggerServiceModule
{
    public static IServiceCollection AdicionaDependenciaLoggerService(this IServiceCollection services)
    {
        services.AddSingleton<ILogger>(Log.Logger);
        services.AddScoped<ISerilogLoggerService, SerilogLoggerService>();
        return services;
    }
}