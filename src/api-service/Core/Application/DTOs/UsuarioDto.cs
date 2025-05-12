using Domain.Entities;

namespace Application.DTOs;

public record LerUsuarioDto(
    string Email,
    string Password,
    string[] Roles
    );
    
public record NovoUsuarioDto(
    string Email,
    string Password,
    string[] Roles
    );

public record SolicitarTokenDto(
    string Email, 
    string Password);

public static class UsuarioDtoAdapter
{
    public static LerUsuarioDto ConverteParaLerUsuarioDto(this Usuario usuario)
    {
        return new LerUsuarioDto(usuario.Email, usuario.Password, usuario.Roles);
    }

    public static Usuario ConverteParaEntidadeUsuario(this NovoUsuarioDto usuario)
    {
        return new Usuario(usuario.Email, usuario.Password, usuario.Roles);
    }
}