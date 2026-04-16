using DebitosTributarios.Domain.DTOs;
using DebitosTributarios.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DebitosTributarios.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ContribuinteController(IContribuinteApplicationService contribuinteService) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Novo([FromBody]ContribuinteNovoDto dto)
        {
            await contribuinteService.NovoContribuinte(dto);

            return Ok();
        }
    }
}
