using Application.DTOs;
using Application.Ports;
using Domain.Entities;
using Domain.Ports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StartRiderApi.Controllers
{
    [Route("api/locatario")]
    [ApiController]
    public class LocatarioController(
        ILocatarioUseCase _locatarioUseCase,
        ISerilogLoggerService _logger
        ) : ControllerBase
    {
        /// <summary>
        /// Cadastra um novo locatario na base de dados.
        /// </summary>
        /// <param name="novoLocatario"></param>
        /// <returns>Status Code 201 Created</returns>
        [Authorize(Roles = "Admin,Usuario")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public async Task<IActionResult> CadastrarNovoLocatarioAsync([FromBody]NovoLocatarioDto novoLocatario)
        {
            try
            {
                await _locatarioUseCase.CadastrarNovoLocatarioAsync(novoLocatario);
                return Created();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro no endpoint /POST /api/locatario, mensagem:{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Atualiza a foto da cnh do locatario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="atualizaFotoCnhLocatarioDto"></param>
        /// <returns>Status 204 NoContent</returns>
        [Authorize(Roles = "Admin,Usuario")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarFotoCnhLocatarioAsync([FromRoute] int id, [FromBody] AtualizaFotoCnhLocatarioDto atualizaFotoCnhLocatarioDto)
        {
            try
            {
                await _locatarioUseCase.AtualizaFotoCnhLocatarioAsync(id, atualizaFotoCnhLocatarioDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro no endpoint /PUT /api/locatario, mensagem:{ex.Message}");
                throw;
            }
        }
    }
}
