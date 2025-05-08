using Application.DTOs;
using Application.Ports;
using Domain.Ports;
using Newtonsoft.Json;

namespace Application.UserCases;

public class VeiculoUseCase(
    IVeiculoRepositoryPort _veiculoRepository,
    ISerilogLoggerService _logger,
    IPublishServicePort _publishService
    ) : IVeiculoUseCase
{
    public async Task<List<LerVeiculoDto>> ListarTodosOsVeiculosCadastradosAsync()
    {
        try
        {
            var veiculos = await _veiculoRepository.RecuperaTodosOsVeiculosAsync();
            
            if(veiculos == null)
                _logger.LogInfo("Nenhum veiculo encontrado na base.");
            
            return veiculos.ConverterParaListaLerVeiculoDto();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao tentar recuperar veiculos cadastrados, mensagem: {ex.Message}");
            throw;
        }
    }

    public async Task<LerVeiculoDto> RecuperaVeiculoPorIdAsync(int id)
    {
        try
        {
            var veiculo = await _veiculoRepository.RecuperaVeiculoPorIdAsync(id);
            
            if(veiculo == null)
                _logger.LogInfo($"Nenhum veiculo com id:{id} encontrado na base.");
            
            return veiculo.ConverterParaLerVeiculoDto();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao tentar recuperar veiculo pelo id, mensagem:{ex.Message}");
            throw;
        }
    }

    public async Task<LerVeiculoDto> RecuperaVeiculoPelaPlacaAsync(string placa)
    {
        try
        {
            var veiculo = await _veiculoRepository.RecuperaVeiculoPorPlacaAsync(placa);
            
            if(veiculo == null)
                _logger.LogInfo($"Nenhum veiculo com placa:{placa} encontrado na base.");
            
            return veiculo.ConverterParaLerVeiculoDto();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao tentar recuperar veiculo pelo id, mensagem:{ex.Message}");
            throw;
        }
    }

    public async Task AtualizaDadosVeiculoAsync(int id, AtualizaVeiculoDto veiculo)
    {
        try
        {
            await _veiculoRepository.AtualizaDadosVeiculoAsync(id, veiculo.Placa, veiculo.NumeroRenavam, veiculo.Cor,veiculo.Modelo);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao tentar atualizar as informações do veiculo de id:{id}, mensagem:{ex.Message}");
            throw;
        }
    }

    public async Task CadastrarNovoVeiculoAsync(NovoVeiculoDto veiculoDto)
    {
        try
        {
            var placaJaCadastrada = await PlacaCadastrada(veiculoDto.Placa);

            if (!placaJaCadastrada)
            {
                var mensagem = JsonConvert.SerializeObject(veiculoDto);
                await _publishService.PublicaMensagemRabbitMqAsync(mensagem);   
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao tentar cadastrar o veiculo:{veiculoDto.Modelo}, mensagem: {ex.Message} ");
            throw;
        }
    }

    public async Task DeletarUmVeiculoCadastradoAsync(int id)
    {
        try
        {
            var veiculo = await _veiculoRepository.RecuperaVeiculoPorIdAsync(id);
            
            if(veiculo != null)
                await _veiculoRepository.DeletarVeiculoCadastradoAsync(veiculo);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao tentar deletar um veiculo, mensagem: {ex.Message} ");
            throw;
        }
    }

    private async Task<bool> PlacaCadastrada(string placa)
    {
        try
        {
            bool existeCadastro = false;
            var veiculo = await _veiculoRepository.RecuperaVeiculoPorPlacaAsync(placa);

            if (veiculo != null)
            {
                existeCadastro = true;
                _logger.LogWarning($"Placa: {placa} já esta cadastrada no sistema.");
            }

            return existeCadastro;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao tentar verificar se a placa já esta cadastrada, mensagem:{ex.Message}");
            throw;
        }
    }
}