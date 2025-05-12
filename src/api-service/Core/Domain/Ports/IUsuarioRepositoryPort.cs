using Domain.Entities;

namespace Domain.Ports;

public interface IUsuarioRepositoryPort
{
    Task<Usuario> RecuperaUsuarioPorEmailAsync(string email);
    Task CadastraNovoUsuarioAsync(Usuario usuario);
}