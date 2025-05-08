using System.Text;
using Domain.Ports;
using RabbitMQ.Client;

namespace RabbitMqPublishService.Veiculo;

public class PublishService(
    ISerilogLoggerService _serilogLogger
    ) : IPublishServicePort
{
    public async Task PublicaMensagemRabbitMqAsync(string mensagem)
    {
        try
        {
            var factory = new ConnectionFactory{HostName = "localhost"};
            await using var connection =  await factory.CreateConnectionAsync();
            await using var chanel = await connection.CreateChannelAsync();

            await chanel.QueueDeclareAsync(
                queue: "veiculo",
                durable: false,
                exclusive:false,
                autoDelete:false,
                arguments: null);
        
            var body = Encoding.UTF8.GetBytes(mensagem);

            await chanel.BasicPublishAsync(exchange:string.Empty, routingKey:"veiculo",body:body);
        
            _serilogLogger.LogInfo("Veiculo publicado na fila.");
        }
        catch (Exception ex)
        {
            _serilogLogger.LogError($"Ocorreu um erro ao tentar publicar mensagem na fila, mensagem:{ex.Message}");
            throw;
        }
        
    }
}