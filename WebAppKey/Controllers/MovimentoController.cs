using System.Collections;
using System.Collections.Immutable;
using System.Linq.Expressions;
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
            double somaQuantidade = 0.0;
            var receitas = await _movimentoServices.GetMovimentoLavouraSafra(idSafra, idLavoura, eTipoMovimento.tmSaida);
            var despesas = await _movimentoServices.GetMovimentoLavouraSafra(idSafra, idLavoura, eTipoMovimento.tmEntrada);
            double totaldespesas = despesas.Select(m => m.Total).DefaultIfEmpty(0.0).Sum();
            double totalreceita = receitas.Select(m => m.Total).DefaultIfEmpty(0.0).Sum();
            
            double valorLucro = (totalreceita - totaldespesas);
            double totalArea = receitas.Sum(a => a.MovimentoLavoura.Lavoura.AreaHa);
            var itens = receitas.Select(m => m.Itens).ToList();
            foreach (var movItem in itens)
            {
                somaQuantidade +=((movItem).Select(i => i.Quantidade).DefaultIfEmpty(0.0).Sum());
            }

            double mediaSacaHa = somaQuantidade / totalArea; 
            
            return Ok(new
            {
                Lucro = valorLucro,
                AreaHa = totalArea,
                MediaSacasHa = mediaSacaHa,
                Despesas = despesas,
                Colheita = receitas
            });
            
        }        
    }