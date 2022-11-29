using Microsoft.AspNetCore.Mvc;
using WebAppKey.Models;
using WebAppKey.Data;
using Microsoft.EntityFrameworkCore;
using WebAppKey.DTO;
using WebAppKey.Models.Enum;
using WebAppKey.Services;
using WebAppKey.Services.Interfaces;

namespace WebAppKey.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoServices;
        private readonly DataContext _context;

        public ProdutoController(IProdutoService produtoService, DataContext context)
        {
            _context = context;
            _produtoServices = produtoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Produto>>> Get()
        {
            var produtos = await _produtoServices.GetAll();
            return Ok(produtos);
        }
        

        [HttpGet]
        [Route("GetById")]
        public async Task<Produto> GetById(int produtoId)
        {
            var produto = await _produtoServices.GetById(produtoId);
            return produto;
        }
      
       [HttpPost]
       public async Task<ActionResult<Produto>> Create(ProdutoDTO produtoDto)
       {
          var produto = await _produtoServices.CreateProduto(produtoDto);
          return Ok(produto);
       }

       [HttpPut("{id}")]
       public async Task<ActionResult<List<Produto>>> Put(ProdutoDTO produtoDto, int id)
        {
            if (ModelState.IsValid)
            {
                var produto  = await _produtoServices.UpdateProduto(id, produtoDto);
                return Ok(produto); 
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

       [HttpDelete("{id}")]
       public async Task<ActionResult<List<Produto>>> Delete(int id)
       {
           try
           {
               await _produtoServices.DeleteById(id);
               return Ok("Produto Deletado!");
           } catch(Exception  e)
           {
               throw new Exception($"Erro ao Deletar Produto {id}. \n" + e.Message);
           }
       }

    }
}
