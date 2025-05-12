using System.Text;
using Application.DTOs;
using Application.Ports;
using Domain.Ports;

namespace Application.UserCases;

public class UsuarioUseCase(
    IUsuarioRepositoryPort _usuarioRepository,
    ISerilogLoggerService _logger
    ) : IUsuarioUseCase
{
    public async Task<LerUsuarioDto> RecuperaUsuarioPorEmailAsync(string email)
    {
        try
        {
            var usuario = await _usuarioRepository.RecuperaUsuarioPorEmailAsync(email);
            return usuario.ConverteParaLerUsuarioDto();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao tentar recuperar usuario, email: {email}, mensagem: {ex.Message}");
            throw;
        }
    }

    public async Task CadastraNovoUsuarioAsync(NovoUsuarioDto usuarioDto)
    {
        try
        {
            var novoUsuario = CodificaSenhaUsuario(usuarioDto);
            await _usuarioRepository.CadastraNovoUsuarioAsync(novoUsuario.ConverteParaEntidadeUsuario());
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao tentar cadastrar novo usuario, mensagem: {ex.Message}");
            throw;
        }
    }

    private NovoUsuarioDto CodificaSenhaUsuario(NovoUsuarioDto usuario)
    {
        byte[] senhaAsByte = Encoding.UTF8.GetBytes(usuario.Password);
        var senhaCodificada =  Convert.ToBase64String(senhaAsByte);
        return new NovoUsuarioDto(usuario.Email, senhaCodificada, usuario.Roles);
    }
}