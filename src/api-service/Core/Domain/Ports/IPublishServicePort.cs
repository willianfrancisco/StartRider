using Domain.Entities;

namespace Domain.Ports;

public interface IPublishServicePort
{
    Task PublicaMensagemRabbitMqAsync(string mensagem);
}