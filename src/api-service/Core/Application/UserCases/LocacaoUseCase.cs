using Application.DTOs;
using Application.Ports;
using Domain.Ports;

namespace Application.UserCases;

public class LocacaoUseCase(
    ILocacaoRepositoryPort _locaoRepository,
    ILocatarioRepositoryPort _locatarioRepository,
    IVeiculoRepositoryPort _veiculoRepository,
    ISerilogLoggerService _logger
    ) : ILocacaoUseCase
{
    public async Task<LerLocacaoDto> RecuperaLocacaoPorIdAsync(int id)
    {
        try
        {
            var locacao = await _locaoRepository.RecuperaLocacaoPorIdAsync(id);
            return locacao.ConverterParaLerLocacaoDto();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao tentar Recuperar uma locacao por id {id}, mensagem: {ex.Message}");
            throw;
        }
    }

    public async Task CadastrarNovaLocacaoAsync(NovaLocacaoDto novaLocacao)
    {
        try
        {
            bool locatarioVeiculoCadastrado = await VerificaCadastroLocatarioVeiculo(novaLocacao.LocatarioId, novaLocacao.VeiculoId);
            
            if(locatarioVeiculoCadastrado)
                await _locaoRepository.CadastrarNovaLocacaoAsync(novaLocacao.ConverterParaLocacaoEntity());
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao tentar cadastrar uma nova Locacao, mensagem: {ex.Message}");
            throw;
        }
    }

    public async Task AtualizaDataDevolucaoLocacaoAsync(int id,AtualizaDataDevolucaoLocacaoDto dataDevolucao)
    {
        try
        {
            var locacao = await _locaoRepository.RecuperaLocacaoPorIdAsync(id);

            if (locacao != null)
            {
                await _locaoRepository.AtualizaDataDevolucaoLocacaoAsync(dataDevolucao.DataDevolucao, locacao);
            }
            else
            {
                _logger.LogWarning($"Locacao {id} não localizada para atualizacao.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao tentar atualizar a data devolução da locacao, mensagem: {ex.Message}");
            throw;
        }
    }

    private async Task<bool> VerificaCadastroLocatarioVeiculo(int locatarioId, int veiculoId)
    {
        bool possuiCadastro = false;
        
        var locatario = await _locatarioRepository.RecuperaLocatarioPorIdAsync(locatarioId);
        var veiculo = await _veiculoRepository.RecuperaVeiculoPorIdAsync(veiculoId);
        
        if(locatario == null)
            _logger.LogWarning($"Locatario com id:{locatarioId} não localizado, por favor validar id");
        
        
        if(veiculo == null)
            _logger.LogWarning($"Veiculo com id:{veiculoId} não localizado, por favor validar id");
        
        if(locatario != null && veiculo != null)
            possuiCadastro = true;
        
        return possuiCadastro;
    }
}