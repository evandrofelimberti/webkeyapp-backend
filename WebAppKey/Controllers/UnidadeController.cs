using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppKey.Models;
using WebAppKey.Data;
using WebAppKey.DTO;
using Microsoft.EntityFrameworkCore;
using WebAppKey.Services;
using WebAppKey.Services.Interfaces;

namespace WebAppKey.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadeController : ControllerBase
    {

        private readonly IUnidadeService _unidadeService;

        public UnidadeController(IUnidadeService unidadeService)
        {
            _unidadeService = unidadeService;
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Unidade>> GetUnidade(int id)
        {
            var unidade = await _unidadeService.GetById(id);
            if (unidade == null)
            {
                return NotFound("Unidade não encontrado pelo id informado");
            }
            return Ok(unidade);
        }        
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Unidade>>> GetAll()
        {
           var unidade = await _unidadeService.GetAll();
           if (unidade == null)
           {
               return BadRequest();
           }
           return Ok(unidade.ToList());           
           
        }
        
      [HttpPost]
      public async Task<IActionResult> PostProduto(UnidadeDto unidadeDto)
      {
          var unidade = new Unidade
          {
              Descricao = unidadeDto.Descricao,
              Sigla = unidadeDto.Sigla
          };
          
          if (unidade == null)
          {
              return BadRequest("Unidade é null");
          }            
          await _unidadeService.Add(unidade);
          return CreatedAtAction(nameof(GetUnidade), new { Id = unidade.Id }, unidade);
      }      
        
        [HttpDelete("{id}")]
        public async  Task<ActionResult<Unidade>> Delete(int id)
        {
            try
            {
                await _unidadeService.DeleteById(id);
                return Ok("Unidade Deletada!");
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao Deletar a Unidade {id} \n" + e.Message);
            }

        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UnidadeDto unidadeDto)
        {
            try
            {
                var unidade = await _unidadeService.UpdateUnidade(id, unidadeDto);
                return Ok(unidade);

            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao Atualizar a Unidade{id} \n"+ e.Message);
            }

        }        
        
    }
}
