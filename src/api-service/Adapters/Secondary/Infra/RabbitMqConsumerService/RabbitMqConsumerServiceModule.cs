using Microsoft.Extensions.DependencyInjection;
using RabbitMqConsumerService.VeiculoConsumer;

namespace RabbitMqConsumerService;

public static class RabbitMqConsumerServiceModule
{
    public static IServiceCollection AdicionaDependenciasRabbitMqConsumerService(this IServiceCollection services)
    {
        services.AddHostedService<VeiculoConsumerService>();
        return services;
    }
}