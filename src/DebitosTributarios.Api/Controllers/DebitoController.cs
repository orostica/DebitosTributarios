using DebitosTributarios.Domain.DTOs;
using DebitosTributarios.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DebitosTributarios.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class DebitosController(
        IDebitoApplicationService debitoService,
        ILogger<DebitosController> logger) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Cadastrar([FromBody] DebitoNovoDto dto)
        {
            // Validações básicas de entrada — retornam 400 com detalhes
            if (dto.Valor <= 0)
                return BadRequest(new { erro = "Valor deve ser maior que zero." });

            if (dto.DataVencimento == default)
                return BadRequest(new { erro = "Data de vencimento é obrigatória." });

            try
            {
                var id = await debitoService.NovoDebitoAsync(dto);

                logger.LogInformation("Débito {Id} criado para contribuinte {ContribuinteId} via API.", id, dto.ContribuinteId);

                // 201 com o ID do débito gerado no corpo
                return Created(string.Empty, new { id });
            }
            catch (InvalidOperationException ex)
            {
                // Contribuinte não encontrado ou outra regra de negócio que falhou
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}