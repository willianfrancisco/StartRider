using System.Net;
using Application.DTOs;
using Application.Ports;
using Domain.Entities;
using Domain.Ports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StartRiderApi.Controllers
{
    [Route("api/veiculo")]
    [ApiController]
    public class VeiculoController(
        IVeiculoUseCase _veiculoUseCase,
        ISerilogLoggerService _logger
        ) : ControllerBase
    {
        /// <summary>
        /// Consulta todos os veículos cadastrados na base de dados.
        /// </summary>
        /// <returns>Uma lista de veículos</returns>
        [Authorize(Roles = "Admin,Usuario")]
        [ProducesResponseType(typeof(List<LerVeiculoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IActionResult> RetornarTodosOsVeiculosAsync()
        {
            try
            {
                var veiculos = await _veiculoUseCase.ListarTodosOsVeiculosCadastradosAsync();
                
                if(veiculos == null)
                    return NotFound();
                
                return Ok(veiculos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro no endpoint /GET /api/veiculo/, mensagem:{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Consulta veículo na base buscando pelo Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Veículo com o id especificado</returns>
        [Authorize(Roles = "Admin,Usuario")]
        [ProducesResponseType(typeof(LerVeiculoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{id}")]
        public async Task<IActionResult> RetornarVeiculoConsultandoPorId([FromRoute] int id)
        {
            try
            {
                var veiculo = await _veiculoUseCase.RecuperaVeiculoPorIdAsync(id);
                
                if(veiculo == null)
                    return NotFound();
                
                return Ok(veiculo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro no endpoint /GET /api/veiculo/id/{id} , mensagem:{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Consulta veículo na base buscando pela placa. 
        /// </summary>
        /// <param name="placa"></param>
        /// <returns>Veículo com o placa especificado</returns>
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(LerVeiculoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("placa/{placa}")]
        public async Task<IActionResult> RetornarVeiculoConsultandoPorPlaca([FromRoute] string placa)
        {
            try
            {
                var veiculo = await _veiculoUseCase.RecuperaVeiculoPelaPlacaAsync(placa);
                
                if(veiculo == null)
                    return NotFound();
                
                return Ok(veiculo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro no endpoint /GET /api/veiculo/placa/{placa}, mensagem:{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Cadastra um veículo novo na base de dados.
        /// </summary>
        /// <param name="veiculo"></param>
        /// <returns>Retorna status 201 Created</returns>
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> CadastrarNovoVeiculoAsync([FromBody]NovoVeiculoDto veiculo)
        {
            try
            {
                await _veiculoUseCase.CadastrarNovoVeiculoAsync(veiculo);
                return Created();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro no endpoint /POST /api/veiculo/, mensagem:{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Atualiza os dados da placa, numeroRenavam,cor e modelo na base de dados.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="veiculo"></param>
        /// <returns>Retorna status 204 NoContent</returns>
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarDadosVeiculoAsync([FromRoute]int id, [FromBody]AtualizaVeiculoDto veiculo)
        {
            try
            {
                await _veiculoUseCase.AtualizaDadosVeiculoAsync(id, veiculo);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro no endpoint /PUT /api/veiculo/{id}, mensagem:{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Deleta um veículo da base de dados.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna status 204 NoContent</returns>
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarVeiculoCadastradoAsync([FromRoute] int id)
        {
            try
            {
                await _veiculoUseCase.DeletarUmVeiculoCadastradoAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro no endpoint /DELETE /api/veiculo/{id}, mensagem:{ex.Message}");
                throw;
            }
        }
        
    }
}
