using DataMySql.Context;
using Domain.Entities;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace DataMySql.LocatarioRepository;

public class LocatarioRepositoryPort(
    StartRiderContext _context,
    ISerilogLoggerService _logger
    ) : ILocatarioRepositoryPort
{
    public async Task CadastrarNovoLocatarioAsync(Locatario locatario)
    {
        try
        {
            await _context.Locatarios.AddAsync(locatario);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao tentar cadastrar um novo locatario, mensagem: {ex.Message}");
            throw;
        }
    }

    public async Task<Locatario> RecuperaLocatarioPorIdAsync(int id)
    {
        try
        {
            var locatario = await _context.Locatarios.FirstOrDefaultAsync(l => l.Id == id);
            
            if(locatario == null)
                _logger.LogWarning($"locatario com id: {id} nao localizado.");
            
            return locatario;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao tentar recuperar um veiculo por id, mensagem: {ex.Message}");
            throw;
        }
    }

    public async Task AtualizaFotoCnhLocatarioAsync(int id, string novaFotoCnh,Locatario locatario)
    {
        try
        {
                locatario.AtualizaFotoCnh(novaFotoCnh);
                await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao tentar atualizar a foto da cnh, mensagem:{ex.Message}");
            throw;
        }
    }
}