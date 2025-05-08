using System.ComponentModel;
using System.Text;
using System.Text.Json.Serialization;
using Domain.Entities;
using Domain.Ports;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMqConsumerService.VeiculoConsumer;

public class VeiculoConsumerService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public VeiculoConsumerService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (stoppingToken.IsCancellationRequested) return;

        using var scope = _scopeFactory.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ISerilogLoggerService>();

            var factory = new ConnectionFactory(){HostName = "localhost"};
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
            
            await channel.QueueDeclareAsync(
                queue:"veiculo",
                durable:false, 
                exclusive:false, 
                autoDelete:false, 
                arguments:null,
                cancellationToken:stoppingToken);
        
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    logger.LogInfo(message);
                    var veiculo = JsonConvert.DeserializeObject<Veiculo>(message);
                    
                    if (veiculo != null)
                    {
                        await CadastrarVeiculoNoBanco(veiculo);
                        await channel.BasicAckAsync(ea.DeliveryTag, false);
                    }
                    else
                    {
                        await channel.BasicNackAsync(ea.DeliveryTag, false, false);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError($"Erro ao processar mensagem, mensagem{ex.Message}");
                    await channel.BasicNackAsync(ea.DeliveryTag, false, true);
                }
               
            };
            await channel.BasicConsumeAsync("veiculo", autoAck:false,consumer:consumer);
            
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
    }

    private async Task CadastrarVeiculoNoBanco(Veiculo veiculo)
    {
        using var scope = _scopeFactory.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ISerilogLoggerService>();
        var veiculoRepository = scope.ServiceProvider.GetRequiredService<IVeiculoRepositoryPort>();
        
        await veiculoRepository.CadastrarVeiculoAsync(veiculo);
        logger.LogInfo("Veiculo cadastrado.");
    }
}