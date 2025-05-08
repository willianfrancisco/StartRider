using DataMySql.Context;
using Domain.Entities;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace DataMySql.VeiculoRepository;

public class VeiculoRepositoryPort(
    StartRiderContext _context,
    ISerilogLoggerService _loggerService
    ) : IVeiculoRepositoryPort
{
    public async Task<List<Veiculo>> RecuperaTodosOsVeiculosAsync()
    {
        try
        {
            var veiculos = await _context.Veiculos.ToListAsync();
            
            if(veiculos == null)
                _loggerService.LogWarning("Nenhum veiculo cadastrado ainda.");
            
            return veiculos;
        }
        catch (Exception ex)
        {
            _loggerService.LogError($"Ocorreu um erro ao tentar recuperar todos os veiculos do banco, mensagem:{ex.Message}");
            throw;
        }
    }

    public async Task<Veiculo> RecuperaVeiculoPorIdAsync(int id)
    {
        try
        {
            var veiculo = await _context.Veiculos.FirstOrDefaultAsync(v => v.Id == id);
            
            if(veiculo == null)
                _loggerService.LogWarning($"VeiculoPublish com id: {id} nao localizado.");
            
            return veiculo;
        }
        catch (Exception ex)
        {
            _loggerService.LogError($"Ocorreu um erro ao tentar recuperar um veiculo por id, mensagem: {ex.Message}");
            throw;
        }
    }

    public async Task<Veiculo> RecuperaVeiculoPorPlacaAsync(string placa)
    {
        try
        {
            var veiculo = await _context.Veiculos.FirstOrDefaultAsync(v => v.Placa == placa);
            
            if(veiculo == null)
                _loggerService.LogWarning($"VeiculoPublish com placa: {placa} nao localizado.");
            
            return veiculo;
        }
        catch (Exception e)
        {
            _loggerService.LogError($"Ocorreu um erro ao tentar recuperar um veiculo pela placa, mensagem: {e.Message}");
            throw;
        }
    }

    public async Task AtualizaDadosVeiculoAsync(int id,string placa, string numeroRenavam, string cor, string modelo)
    {
        try
        {
           var veiculo = await _context.Veiculos.FirstOrDefaultAsync(v => v.Id == id);
           
           if(veiculo == null)
               _loggerService.LogWarning($"VeiculoPublish com id: {id} nao localizado para atualizar.");

           if (veiculo != null)
           {
               if(!string.IsNullOrEmpty(numeroRenavam))
                   veiculo.AlteraNumeroRenavam(numeroRenavam);
               
               if(!string.IsNullOrEmpty(cor))
                   veiculo.AlteraCor(cor);
               
               if(!string.IsNullOrEmpty(placa))
                   veiculo.AlteraPlaca(placa);
               
               if(!string.IsNullOrEmpty(modelo))
                   veiculo.AlteraModelo(modelo);
               
               await _context.SaveChangesAsync();
           }
           
        }
        catch (Exception ex)
        {
            _loggerService.LogError($"Ocorreu um erro ao tentar atualizar as informações do veiculo, mensagem: {ex.Message}");
            throw;
        }
    }

    public async Task CadastrarVeiculoAsync(Veiculo veiculo)
    {
        try
        {
            await _context.Veiculos.AddAsync(veiculo);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _loggerService.LogError($"Ocorreu um erro ao tentar cadastrar um novo veiculo, mensagem: {ex.Message}");
            throw;
        }
    }

    public async Task DeletarVeiculoCadastradoAsync(Veiculo veiculo)
    {
        try
        {
                _context.Veiculos.Remove(veiculo);
                await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _loggerService.LogError($"Ocorreu um erro ao tentar deletar um veiculo, mensagem: {e.Message}");
            throw;
        }
    }
}