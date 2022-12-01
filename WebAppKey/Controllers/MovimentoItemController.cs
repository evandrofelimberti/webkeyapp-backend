using Microsoft.AspNetCore.Mvc;
using WebAppKey.DTO;
using WebAppKey.Models;
using WebAppKey.Services.Interfaces;

namespace WebAppKey.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class MovimentoItemController: ControllerBase
    {
        private readonly IMovimentoItemService _movimentoItemServices;

        public MovimentoItemController(IMovimentoItemService movimentoItemService)
        {
            _movimentoItemServices = movimentoItemService;
        }
        
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Movimento>> Delete(MovimentoDTO movimentoDto, int id)
        {
            try
            {
                return await _movimentoItemServices.DeleteMovimentoItem(movimentoDto, id);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }

        }        
        
    }