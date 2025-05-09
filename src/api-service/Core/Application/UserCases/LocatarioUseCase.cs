using Application.DTOs;
using Application.Ports;
using Domain.Ports;

namespace Application.UserCases;

public class LocatarioUseCase(
    ILocatarioRepositoryPort _locatarioRepository,
    ISerilogLoggerService _logger
    ) : ILocatarioUseCase
{
    public async Task CadastrarNovoLocatarioAsync(NovoLocatarioDto novoLocatario)
    {
        try
        {
            await _locatarioRepository.CadastrarNovoLocatarioAsync(novoLocatario.ConverterLocatarioDtoParEntidade());
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao tentar cadastrar o locatario {novoLocatario.Nome}, {novoLocatario.Cpf}, mensagem: {ex.Message}");
            throw;
        }
    }

    public async Task AtualizaFotoCnhLocatarioAsync(int id, AtualizaFotoCnhLocatarioDto novoLocatario)
    {
        try
        {
            var locatario = await _locatarioRepository.RecuperaLocatarioPorIdAsync(id);

            if (locatario != null)
            {
                await _locatarioRepository.AtualizaFotoCnhLocatarioAsync(id, novoLocatario.NovaFotoCnh, locatario);
            }
            else
            {
                _logger.LogWarning($"Locatario com id: {id} nao localizado para atualizar a foto da cnh.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao tentar atualizar a foto da cnd do locatario {id}, mensagem: {ex.Message}");
            throw;
        }
    }
}