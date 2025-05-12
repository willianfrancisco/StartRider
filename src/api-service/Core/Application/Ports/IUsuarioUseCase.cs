using Application.DTOs;

namespace Application.Ports;

public interface IUsuarioUseCase
{
    Task<LerUsuarioDto> RecuperaUsuarioPorEmailAsync(string email);
    Task CadastraNovoUsuarioAsync(NovoUsuarioDto usuarioDto);
}