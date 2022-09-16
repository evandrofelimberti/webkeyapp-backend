using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppKey.Models;
using WebAppKey.Data;
using WebAppKey.DTO;
using Microsoft.EntityFrameworkCore;
using WebAppKey.Services.Interfaces;

namespace WebAppKey.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TipoMovimentoController : ControllerBase
    {
        private readonly ITipoMovimentoService _tipoMovimentoService;
        
        public TipoMovimentoController(ITipoMovimentoService tipoMovimentoService)
        {
            _tipoMovimentoService = tipoMovimentoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoMovimento>>> Get()
        {
            var tipos = await _tipoMovimentoService.GetAll();
            return Ok(tipos);  
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoMovimento>> GetById(int id)
        {
            var tipo = await _tipoMovimentoService.FirstOrDefault(t => t.Id == id);
            return Ok(tipo);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TipoMovimento>> Put(int id, TipoMovimentoDTO tipoMovimentoDto)
        {
            var tipoMovimento = await _tipoMovimentoService.GetById(id);
            tipoMovimento.FromTipoMovimentoDto(tipoMovimentoDto);
            await _tipoMovimentoService.Update(tipoMovimento);
            return Ok(tipoMovimento);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoMovimento>> Delete(int id)
        {
            await _tipoMovimentoService.DeleteById(id);
            return Ok("Tipo Movimento deletado!");
        }
        
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<TipoMovimento>> Post(TipoMovimentoDTO tipoMovimentoDto)
        {
            if (ModelState.IsValid)
            {
                var tipo = new TipoMovimento();
                tipo.FromTipoMovimentoDto(tipoMovimentoDto);
                await _tipoMovimentoService.Add(tipo);
                return Ok(tipo);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }          
    }
}    