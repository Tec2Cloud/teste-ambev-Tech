using API.Dtos;
using API.Extensions;
using AutoMapper;
using Business.Interfaces;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/vendas")]
    [Produces("application/json")]
    public class VendaController : ControllerBase
    {
        private readonly IVendaService _vendaService;
        private readonly IMapper _mapper;

        public VendaController(IVendaService vendaService, IMapper mapper)
        {
            _vendaService = vendaService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<RegistrarVendaDto>> RegistrarVenda([FromBody] RegistrarVendaDto registrarVendaDto)
        {
            var itens = registrarVendaDto.Itens.ToItemVendaList();

            var result = await _vendaService.RegistrarVenda(
                registrarVendaDto.ClienteId,
                registrarVendaDto.NomeCliente,
                registrarVendaDto.FilialId,
                registrarVendaDto.NomeFilial,
                itens
            );

            if (!result.IsSuccess)
                return BadRequest(new { message = result.Error });

            var vendaDto = _mapper.Map<RegistrarVendaDto>(result.Value);

            return CreatedAtAction(nameof(ObterVendaPorId), new { vendaId = result.Value.Id }, vendaDto);
        }

        [HttpPut("{vendaId}")]
        public async Task<IActionResult> AtualizarVenda(Guid vendaId, [FromBody] AtualizarVendaDto atualizarVendaDto)
        {

            var itens = atualizarVendaDto.Itens.ToItemVendaList();

            var result = await _vendaService.AtualizarVenda(vendaId, itens);

            if (!result.IsSuccess)
            {
                return BadRequest(new { message = result.Error });
            }

            return Ok(result.Value);
        }

        [HttpGet("{vendaId}")]
        public async Task<IActionResult> ObterVendaPorId(Guid vendaId)
        {
            var venda = await _vendaService.ObterVendaPorId(vendaId);

            if (venda == null)
                return NotFound(new { message = "Venda não encontrada." });

            return Ok(venda);
        }

        [HttpPut("{vendaId}/cancelar")]
        public async Task<IActionResult> CancelarVenda(Guid vendaId)
        {
            await _vendaService.CancelarVenda(vendaId);
            return NoContent();
        }
    }
}
