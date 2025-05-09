using Application.DTOs;

namespace Application.Ports;

public interface ILocatarioUseCase
{
    Task CadastrarNovoLocatarioAsync(NovoLocatarioDto novoLocatario);
    Task AtualizaFotoCnhLocatarioAsync(int id, AtualizaFotoCnhLocatarioDto novoLocatario);
}