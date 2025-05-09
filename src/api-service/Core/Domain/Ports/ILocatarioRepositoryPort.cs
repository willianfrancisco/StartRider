using Domain.Entities;

namespace Domain.Ports;

public interface ILocatarioRepositoryPort
{
    Task CadastrarNovoLocatarioAsync(Locatario locatario);
    Task<Locatario> RecuperaLocatarioPorIdAsync(int id);
    Task AtualizaFotoCnhLocatarioAsync(int id, string novaFotoCnh, Locatario locatario);
}