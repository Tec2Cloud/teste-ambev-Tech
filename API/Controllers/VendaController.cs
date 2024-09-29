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
            try
            {
                var itens = registrarVendaDto.Itens.ToItemVendaList();

                var venda = await _vendaService.RegistrarVenda(
                    registrarVendaDto.ClienteId,
                    registrarVendaDto.NomeCliente,
                    registrarVendaDto.FilialId,
                    registrarVendaDto.NomeFilial,
                    itens
                );

                var vendaDto = _mapper.Map<RegistrarVendaDto>(venda);

                return CreatedAtAction(nameof(ObterVendaPorId), new { vendaId = venda.Id }, vendaDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{vendaId}")]
        public async Task<IActionResult> AtualizarVenda(Guid vendaId, [FromBody] AtualizarVendaDto atualizarVendaDto)
        {
            try
            {
                var itens = atualizarVendaDto.Itens.ToItemVendaList();

                var vendaAtualizada = await _vendaService.AtualizarVenda(vendaId, itens);

                return Ok(vendaAtualizada);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{vendaId}")]
        public async Task<IActionResult> ObterVendaPorId(Guid vendaId)
        {
            try
            {
                var venda = await _vendaService.ObterVendaPorId(vendaId);

                if (venda == null)
                {
                    return NotFound(new { message = "Venda não encontrada." });
                }

                return Ok(venda);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{vendaId}/cancelar")]
        public async Task<IActionResult> CancelarVenda(Guid vendaId)
        {
            try
            {
                await _vendaService.CancelarVenda(vendaId);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
