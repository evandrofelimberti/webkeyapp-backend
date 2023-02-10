using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebAppKey.DTO;
using WebAppKey.Models;
using WebAppKey.Models.Enum;
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
        public async  Task<ActionResult<IEnumerable<Movimento>>> Get()
        {
            return Ok(await _movimentoServices.GetAll());
            
        }

        [HttpGet]        
        [Route("{id}")]
        public async Task<ActionResult<Movimento>> GetById(int id)
        {
            var movimento =  await _movimentoServices.GetById(id);
            return movimento;
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
        
        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<Movimento>> Patch(MovimentoDTO movimentoDto, int id)
        {
            var movimento = await _movimentoServices.UpdateMovimento(id, movimentoDto);
            return movimento;
        }        

        [HttpDelete]
        [Route("{id}")]
        public async Task<HttpStatusCode> Delete(int id)
        {
            await _movimentoServices.DeleteById(id);
            return HttpStatusCode.OK;
        }
        
        [HttpGet]
        [Route("LavouraSafra")]
        public async Task<IActionResult> Get(int idSafra, int idLavoura)
        {
            var totaldespesas = 0.0;
            var totalreceita = 0.0;
            
            var despesas = await _movimentoServices.GetMovimentoLavouraSafra(idSafra, idLavoura, eTipoMovimento.tmEntrada);
            var receitas = await _movimentoServices.GetMovimentoLavouraSafra(idSafra, idLavoura, eTipoMovimento.tmSaida);

            totaldespesas = (despesas as ICollection<Movimento>).Sum(d => d.Total);
            
            totalreceita = (receitas as ICollection<Movimento>).Sum(d => d.Total);

            return Ok(new
            {
                Lucro = (totalreceita - totaldespesas),
                Despesas = despesas,
                Colheita = receitas
            });
            
        }        
    }