using Application.DTOs;

namespace Application.Ports;

public interface ILocacaoUseCase
{
    Task<LerLocacaoDto> RecuperaLocacaoPorIdAsync(int id);
    Task CadastrarNovaLocacaoAsync(NovaLocacaoDto novaLocacao);
    Task AtualizaDataDevolucaoLocacaoAsync(int id, AtualizaDataDevolucaoLocacaoDto dataDevolucao);
}