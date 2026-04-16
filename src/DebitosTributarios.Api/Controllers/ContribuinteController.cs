using DebitosTributarios.Domain.DTOs;
using DebitosTributarios.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DebitosTributarios.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ContribuinteController(IContribuinteApplicationService contribuinteService, ILogger<ContribuinteController> logger) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CadastrarContribuinte([FromBody] ContribuinteNovoDto dto)
        {
            // Validação dos campos obrigatórios - retorna 400 com detalhes
            if (string.IsNullOrWhiteSpace(dto.Nome))
                return BadRequest(new { erro = "Nome é obrigatório." });

            if (string.IsNullOrWhiteSpace(dto.CpfCnpj))
                return BadRequest(new { erro = "CPF/CNPJ é obrigatório." });

            try
            {
                var id = await contribuinteService.NovoContribuinteAsync(dto);

                logger.LogInformation("Contribuinte {Id} criado via API.", id);

                // 201 Criado com a localização do recurso e o ID no corpo
                return CreatedAtAction(
                    nameof(ObterContribuinte),
                    new { id },
                    new { id }
                );
            }
            catch (InvalidOperationException ex)
            {
                // CPF/CNPJ duplicado ou outra regra de negócio que falhou
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ContribuinteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ObterContribuinte(int id)
        {
            var resumo = await contribuinteService.ObterContribuinteAsync(id);

            //204 para contribuinte inexistente
            if (resumo is null)
                return NoContent();

            return Ok(resumo);
        }
    }
}
