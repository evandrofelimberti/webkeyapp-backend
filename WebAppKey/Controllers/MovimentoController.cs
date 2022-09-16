using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAppKey.DTO;
using WebAppKey.Models;
using WebAppKey.Services.Interfaces;

namespace WebAppKey.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class MovimentoController: ControllerBase
    {
        private readonly IMovimentoService _movimentoServices;

        public MovimentoController(IMovimentoService movimentoService)
        {
            _movimentoServices = movimentoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Movimento>>> Get()
        {
            var movimentos = await _movimentoServices.GetAll();
            return Ok(movimentos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Movimento>> GetById(int id)
        {
            var movimento =  await  _movimentoServices.GetById(id);
            return Ok(movimento);
        }

        [HttpPost]
        public async Task<ActionResult<Movimento>> Create(MovimentoDTO movimentoDto)
        {
            try
            {
                var movimento = await _movimentoServices.CreateMovimento(movimentoDto);
                return Ok(movimento);
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao Salvar Movimento \n" + e.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Movimento>> Put(MovimentoDTO movimentoDto, int id)
        {
            var movimento = await _movimentoServices.UpdateMovimento(id, movimentoDto);
            return movimento;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Movimento>> Delete(int id)
        {
            await _movimentoServices.DeleteById(id);
            return Ok("Movimento deletado!");
        }        
    }