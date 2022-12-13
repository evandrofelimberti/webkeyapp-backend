using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebAppKey.DTO;
using WebAppKey.Models;
using WebAppKey.Services.Interfaces;

namespace WebAppKey.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LavouraController : ControllerBase
{
    public readonly ILavouraService _lavouraService;

    public LavouraController(ILavouraService lavouraService)
    {
        _lavouraService = lavouraService;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Lavoura>> GetById(int id)
    {
        var lavoura = await _lavouraService.GetById(id);
        return Ok(lavoura);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Lavoura>>> GetAll()
    {
        var lavoura = await _lavouraService.GetAll();
        if (lavoura == null)
        {
            return BadRequest();
        }

        return Ok(lavoura.ToList());
    }

    [HttpPost]
    public async Task<ActionResult<Lavoura>> Create(LavouraDTO lavouraDto)
    {
        try
        {
            var lavoura = await _lavouraService.createLavoura(lavouraDto);
            return Ok(lavoura);
        }
        catch (Exception e)
        {
            throw new Exception($"Erro ao Salvar \n" + e.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<Lavoura>> Put(int id, LavouraDTO lavouraDto)
    {
        var lavoura = await _lavouraService.updateLavoura(id, lavouraDto);
        return Ok(lavoura);
    }

    [HttpDelete]
    public async Task<HttpStatusCode> Delete(int id)
    {
        await _lavouraService.DeleteById(id);
        return HttpStatusCode.OK;
    }
}