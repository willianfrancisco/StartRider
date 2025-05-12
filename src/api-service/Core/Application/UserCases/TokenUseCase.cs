using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTOs;
using Application.Ports;
using Domain.Entities;
using Domain.Ports;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.UserCases;

public class TokenUseCase : ITokenUseCase
{
    private readonly JwtSettings _jwtSettings;
    private readonly string _secretKey;
    private readonly int _expiryInMinutes;
    private readonly IUsuarioUseCase _usuarioUseCase;
    private readonly ISerilogLoggerService _logger;
    private ClaimsIdentity _claims;

    public TokenUseCase(IOptions<JwtSettings> jwtOptions, IUsuarioUseCase usuarioUseCase, ISerilogLoggerService logger)
    {
        _jwtSettings = jwtOptions.Value;
        _secretKey = _jwtSettings.SecretKey;
        _expiryInMinutes = _jwtSettings.ExpiryInMinutes;
        _usuarioUseCase = usuarioUseCase;
        _logger = logger;
    }
    
    public async Task<string> GenerateTokenAsync(SolicitarTokenDto usuario)
    {
        try
        {
            var usuarioValido = await ValidaInformacoesUsuarioAsync(usuario);
            
            if (usuarioValido)
            {
                var handler = new JwtSecurityTokenHandler();
        
                var key = Encoding.UTF8.GetBytes(_secretKey);

                var credentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = _claims,
                    SigningCredentials = credentials,
                    Expires = DateTime.UtcNow.AddMinutes(_expiryInMinutes)
                };

                var token = handler.CreateToken(tokenDescriptor);
        
                return handler.WriteToken(token);
            }
            else
            {
                return new { mensagem="Usuario ou senha Invalidos!" }.ToString();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao tentar recuperar o token, mensagem{ex.Message}");
            throw;
        }
    }

    private static ClaimsIdentity GenerateClaims(LerUsuarioDto usuario)
    {
        var ci = new ClaimsIdentity();
        
        ci.AddClaim(new Claim(ClaimTypes.Name, usuario.Email));
        foreach (var role in usuario.Roles)
        {
            ci.AddClaim(new Claim(ClaimTypes.Role, role));
        }
        
        return ci;
    }

    private async Task<bool> ValidaInformacoesUsuarioAsync(SolicitarTokenDto usuarioDto)
    {
        bool valido = false;
        var usuario = await _usuarioUseCase.RecuperaUsuarioPorEmailAsync(usuarioDto.Email);

        if (usuario != null)
        {
            var passwordBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(usuarioDto.Password));
            if (usuario.Password == passwordBase64 && usuarioDto.Email == usuarioDto.Email)
            {
                valido = true;
                _claims = GenerateClaims(usuario);
            }
        }


        return valido;
    }
}