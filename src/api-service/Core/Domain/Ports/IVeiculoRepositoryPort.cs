using Domain.Entities;

namespace Domain.Ports;

public interface IVeiculoRepositoryPort
{
    Task<List<Veiculo>> RecuperaTodosOsVeiculosAsync();
    Task<Veiculo> RecuperaVeiculoPorIdAsync(int id);
    Task<Veiculo> RecuperaVeiculoPorPlacaAsync(string placa);
    Task AtualizaDadosVeiculoAsync(int id,string placa, string numeroRenavam, string cor, string modelo);
    Task CadastrarVeiculoAsync(Veiculo veiculo);
    Task DeletarVeiculoCadastradoAsync(Veiculo veiculo);
}