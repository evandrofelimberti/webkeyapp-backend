using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                await _movimentoItemServices.DeleteById(id);
                return Ok("true");
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }

        }        
        
    }