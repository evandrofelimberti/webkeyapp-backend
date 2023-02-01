using System;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using WebAppKey.Models;
using WebAppKey.Models.Enum;

namespace WebAppKey.Services;

public class AutenticacaoService
{
    public static string GenerateToken(Usuario usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var key = Encoding.ASCII.GetBytes("ZWRpw6fDo28gZW0gY29tcHV0YWRvcmE=");
            
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, usuario.Nome.ToString()),
                new Claim(ClaimTypes.Role, RoleFactory(usuario.Tipo))
            }),
            Expires = DateTime.UtcNow.AddDays(1),

            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
            
        var token = tokenHandler.CreateToken(tokenDescriptor);
            
        return tokenHandler.WriteToken(token);
    }
    private static string RoleFactory(eTipoUsuario tipoUsuario)
    {
        switch (tipoUsuario)
        {
            case eTipoUsuario.tuAdmin:
                return "Admin";
            default:
                throw new Exception();
        }
    }    
}