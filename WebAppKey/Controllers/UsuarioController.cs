using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppKey.Data;
using WebAppKey.DTO;
using WebAppKey.Models;
using WebAppKey.Services;
using WebAppKey.Services.Interfaces;

namespace WebAppKey.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("auth")]
    public async Task<IActionResult> Auth(UsuarioDTO usuarioDto)
    {
        try
        {
        //var userExists = await _usuarioService.GetUsuario(usuarioDto.Nome);

        var userExists = await _usuarioService.FirstOrDefault(u => u.Nome.ToLower() == usuarioDto.Nome.ToLower());

        if (userExists == null)
            return BadRequest(new { Message = "Usuário e/ou senha está(ão) inválido(s)." });

        if (userExists.Senha != usuarioDto.Senha)
            return BadRequest(new { Message = "Usuário e/ou senha está(ão) inválido(s)." });

        var token = _usuarioService.GenerateToken(userExists);

        userExists.Senha = "";

        return Ok(new
        {
            Token = token,
            Usuario = userExists
        });

        }
        catch (Exception)
        {
            return BadRequest(new { Message = "Ocorreu algum erro interno na aplicação, por favor tente novamente." });
        }
    }

}
