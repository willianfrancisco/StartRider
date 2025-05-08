using Application.DTOs;
using Application.Ports;
using Domain.Entities;
using Domain.Ports;
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
        [HttpGet]
        public async Task<IActionResult> RetornarTodosOsVeiculosAsync()
        {
            try
            {
                var veiculos = await _veiculoUseCase.ListarTodosOsVeiculosCadastradosAsync();
                return Ok(veiculos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro no endpoint /GET /api/veiculo/, mensagem:{ex.Message}");
                throw;
            }
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> RetornarVeiculoConsultandoPorId([FromRoute] int id)
        {
            try
            {
                var veiculo = await _veiculoUseCase.RecuperaVeiculoPorIdAsync(id);
                return Ok(veiculo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro no endpoint /GET /api/veiculo/id/{id} , mensagem:{ex.Message}");
                throw;
            }
        }

        [HttpGet("placa/{placa}")]
        public async Task<IActionResult> RetornarVeiculoConsultandoPorPlaca([FromRoute] string placa)
        {
            try
            {
                var veiculo = await _veiculoUseCase.RecuperaVeiculoPelaPlacaAsync(placa);
                return Ok(veiculo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro no endpoint /GET /api/veiculo/placa/{placa}, mensagem:{ex.Message}");
                throw;
            }
        }

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
