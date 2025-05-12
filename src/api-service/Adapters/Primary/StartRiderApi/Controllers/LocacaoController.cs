using Application.DTOs;
using Application.Ports;
using Domain.Entities;
using Domain.Ports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StartRiderApi.Controllers
{
    [Route("api/locacao")]
    [ApiController]
    public class LocacaoController(
        ILocacaoUseCase _locacaoUseCase,
        ISerilogLoggerService _logger
        ) : ControllerBase
    {
        /// <summary>
        /// Consulta locação na base buscando pelo Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Locacao com o id especificado</returns>
        [Authorize(Roles="Admin,Usuario")]
        [ProducesResponseType(typeof(Locacao), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperaLocacaoPorIdAsync([FromRoute] int id)
        {
            try
            {
                var locacao = await _locacaoUseCase.RecuperaLocacaoPorIdAsync(id);
                
                if(locacao == null)
                    return NotFound();
                
                return Ok(locacao);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro ao chamar o endpoint /GET/{id} api/locacao, mensagem: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Cadastra uma nova Locacao.
        /// </summary>
        /// <param name="novaLocacao"></param>
        /// <returns>Retorna status 201 Created</returns>
        [Authorize(Roles="Admin,Usuario")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> CadastrarNovaLocacaoAsync([FromBody]NovaLocacaoDto novaLocacao)
        {
            try
            {
                await _locacaoUseCase.CadastrarNovaLocacaoAsync(novaLocacao);
                return Created();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro ao chamar o endpoint /POST api/locacao, mensagem: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Atualiza a data devolução da locação
        /// </summary>
        /// <param name="id"></param>
        /// <param name="novaDataDevolucaoLocacao"></param>
        /// <returns>Retorna status 204 NoContent</returns>
        [Authorize(Roles="Admin,Usuario")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizaDataDevolucaoLocacaoAsync([FromRoute]int id, [FromBody]AtualizaDataDevolucaoLocacaoDto novaDataDevolucaoLocacao)
        {
            try
            {
                await _locacaoUseCase.AtualizaDataDevolucaoLocacaoAsync(id, novaDataDevolucaoLocacao);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro ao chamar o endpoint /PUT /api/locacao, mensagem: {ex.Message}");
                throw;
            }
        }
    }
}
