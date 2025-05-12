using Application.DTOs;
using Application.Ports;
using Domain.Ports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StartRiderApi.Controllers
{
    [Route("api/autenticacao")]
    [ApiController]
    public class AutenticacaoController(
        ITokenUseCase _tokenUse,
        ISerilogLoggerService _logger
        ) : ControllerBase
    {
        /// <summary>
        /// Gera o token para acesso aos metodos.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns>Token</returns>
        [HttpPost("gerar-token")]
        public async Task<IActionResult> GerarToken([FromBody] SolicitarTokenDto usuario)
        {
            try
            {
                var token = await _tokenUse.GenerateTokenAsync(usuario);
            
                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro no endpoint /POST /api/autenticacao/gerar-token, mensagem: {ex.Message}");
                throw;
            }
        }
    }
}
