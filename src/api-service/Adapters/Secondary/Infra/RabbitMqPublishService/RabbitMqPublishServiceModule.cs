using Domain.Ports;
using Microsoft.Extensions.DependencyInjection;
using RabbitMqPublishService.Veiculo;

namespace RabbitMqPublishService;

public static class RabbitMqPublishServiceModule
{
    public static IServiceCollection AdicionaDependenciasInfraRabbitMqPublishService(this IServiceCollection services)
    {
        services.AddScoped<IPublishServicePort, PublishService>();
        return services;
    }
}