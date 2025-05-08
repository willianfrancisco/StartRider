using Application.DTOs;

namespace Application.Ports;

public interface IVeiculoUseCase
{
    Task<List<LerVeiculoDto>> ListarTodosOsVeiculosCadastradosAsync();
    Task<LerVeiculoDto> RecuperaVeiculoPorIdAsync(int id);
    Task<LerVeiculoDto> RecuperaVeiculoPelaPlacaAsync(string placa);
    Task AtualizaDadosVeiculoAsync(int id,AtualizaVeiculoDto veiculo);
    Task CadastrarNovoVeiculoAsync(NovoVeiculoDto veiculoDto);
    Task DeletarUmVeiculoCadastradoAsync(int id);
}