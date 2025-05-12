using Application.DTOs;
using Application.Ports;
using Domain.Ports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StartRiderApi.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController(
        IUsuarioUseCase _usuarioUseCase,
        ISerilogLoggerService  _logger
        ) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CadastrarNovoUsuarioAsync([FromBody] NovoUsuarioDto usuarioDto)
        {
            try
            {
                await _usuarioUseCase.CadastraNovoUsuarioAsync(usuarioDto);
                return Created();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorre um erro no endpoint /POST /api/usuario, mensagem:  {ex.Message}");
                throw;
            }
        }
    }
}
