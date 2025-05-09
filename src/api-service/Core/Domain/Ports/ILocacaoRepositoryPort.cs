using Domain.Entities;

namespace Domain.Ports;

public interface ILocacaoRepositoryPort
{
    Task<Locacao> RecuperaLocacaoPorIdAsync(int id);
    Task CadastrarNovaLocacaoAsync(Locacao locacao);
    Task AtualizaDataDevolucaoLocacaoAsync(DateTime dataDevolucao, Locacao locacao);
}