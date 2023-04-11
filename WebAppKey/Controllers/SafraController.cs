using Microsoft.AspNetCore.Mvc;
using WebAppKey.DTO;
using WebAppKey.Models;
using WebAppKey.Services.Interfaces;

namespace WebAppKey.Controllers;

    // GET
    [Route("api/[controller]")]
    [ApiController]

    public class SafraController : ControllerBase
    {
        private readonly ISafraService _safraService;
        
        public SafraController(ISafraService safraService)
        {
            _safraService = safraService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Safra>>> Get()
        {
            var tipos = await _safraService.GetAll();
            return Ok(tipos);  
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Safra>> GetById(int id)
        {
            var safra = await _safraService.FirstOrDefault(t => t.Id == id);
            return Ok(safra);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Safra>> Put(int id, SafraDTO safraDto)
        {
            var safra = await _safraService.updateSafra(id, safraDto);
            return Ok(safra);
        }

        [HttpDelete("{id}")]
        public ActionResult<Safra> Delete(int id)
        {
            _safraService.DeleteById(id);
            return Ok("Safra deletado!");
        }
        
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Safra>> Post(SafraDTO safraDto)
        {
            try
            {
                var safra = await _safraService.createSafra(safraDto);
                return Ok(safra);
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao Salvar \n" + e.Message);
            }

        }          
    }