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

    public class TipoProdutoController : ControllerBase
    {
        private readonly ITipoProdutoService _tipoProdutoService;
        
        public TipoProdutoController(ITipoProdutoService tipoProdutoService)
        {
            _tipoProdutoService = tipoProdutoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoProduto>>> Get()
        {
            var tipos = await _tipoProdutoService.GetAll();
            return Ok(tipos);  
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoProduto>> GetById(int id)
        {
            //var tipoProduto = await _tipoProdutoService.GetById(id);
            var tipoProduto = await _tipoProdutoService.FirstOrDefault(t => t.Id == id);
            return Ok(tipoProduto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TipoProduto>> Put(int id, TipoProdutoDTO tipoProdutoDto)
        {
            var tipoProduto = await _tipoProdutoService.GetById(id);
            tipoProduto.FromTipoProdutoDto(tipoProdutoDto);
            await _tipoProdutoService.Update(tipoProduto);
            return Ok(tipoProduto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoProduto>> Delete(int id)
        {
            await _tipoProdutoService.DeleteById(id);
            return Ok("Tipo Produto deletado!");
        }
        
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<TipoProduto>> Post(TipoProdutoDTO tipoProdutoDto)
        {
            if (ModelState.IsValid)
            {
                var tipo = new TipoProduto();
                tipo.FromTipoProdutoDto(tipoProdutoDto);
                await _tipoProdutoService.Add(tipo);
                return Ok(tipo);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }          
    }
}    