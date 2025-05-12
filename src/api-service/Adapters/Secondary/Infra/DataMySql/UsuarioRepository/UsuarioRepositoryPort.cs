using DataMySql.Context;
using Domain.Entities;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace DataMySql.UsuarioRepository;

public class UsuarioRepositoryPort(
    StartRiderContext _context,
    ISerilogLoggerService _logger
    ) : IUsuarioRepositoryPort
{
    public async Task<Usuario> RecuperaUsuarioPorEmailAsync(string email)
    {
        try
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            
            if(usuario == null)
                _logger.LogWarning($"usuario com email: {email} nao localizado.");
            
            return usuario;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao tentar consultar usuario na base pelo email:{email}, mensagem:{ex.Message}");
            throw;
        }
    }

    public async Task CadastraNovoUsuarioAsync(Usuario usuario)
    {
        try
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao tentar cadastrar um novo usuario, mensagem:{ex.Message}");
            throw;
        }
    }
}