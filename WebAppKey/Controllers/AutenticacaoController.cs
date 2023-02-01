using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppKey.Data;
using WebAppKey.DTO;
using WebAppKey.Services;

namespace WebAppKey.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AutenticacaoController: ControllerBase
{
    protected DataContext _context;

    public AutenticacaoController(DataContext context)
    {
        _context = context;
    }    
    
    [HttpPost]
    [AllowAnonymous]
    [Route("/auth")]
    public async Task<IActionResult> Auth(UsuarioDTO usuarioDto)
    {
        try
        {
            var userExists = _context.Usuario.Where(u => u.Nome.ToLower() == usuarioDto.Nome.ToLower()).FirstOrDefault();

            if (userExists == null)
                return BadRequest(new { Message = "Usuário e/ou senha está(ão) inválido(s)." });
                
            if(userExists.Senha != usuarioDto.Senha)
                return BadRequest(new { Message = "Usuário e/ou senha está(ão) inválido(s)." });

            var token = AutenticacaoService.GenerateToken(userExists);

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