using Application.DTOs;

namespace Application.Ports;

public interface ITokenUseCase
{
    Task<string> GenerateTokenAsync(SolicitarTokenDto usuario);
}