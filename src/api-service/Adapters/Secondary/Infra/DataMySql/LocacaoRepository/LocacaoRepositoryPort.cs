using DataMySql.Context;
using Domain.Entities;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace DataMySql.LocacaoRepository;

public class LocacaoRepositoryPort(
    StartRiderContext _context,
    ISerilogLoggerService _logger
    ) : ILocacaoRepositoryPort
{
    public async Task<Locacao> RecuperaLocacaoPorIdAsync(int id)
    {
        try
        {
            var locacao = await _context.Locacoes.FirstOrDefaultAsync(l => l.Id == id);
            return locacao;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao tentar consultar locacao {id} no banco. mensagem:{ex.Message}");
            throw;
        }
    }

    public async Task CadastrarNovaLocacaoAsync(Locacao locacao)
    {
        try
        {
            await _context.Locacoes.AddAsync(locacao);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
           _logger.LogError($"Ocorreu um erro ao tentar cadastrar locacao no banco. mensagem:{ex.Message}");
            throw;
        }
    }

    public async Task AtualizaDataDevolucaoLocacaoAsync(DateTime dataDevolucao, Locacao locacao)
    {
        try
        {
            locacao.AtualizaDataDevolucao(dataDevolucao);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
           _logger.LogError($"Ocorreu um erro ao tentar atualizar a data devolucao no banco, mensagem:{ex.Message}");
            throw;
        }
    }
}