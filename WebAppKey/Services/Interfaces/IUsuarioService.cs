using Microsoft.AspNetCore.Mvc;
using WebAppKey.DTO;

namespace WebAppKey.Services.Interfaces;
using WebAppKey.Models;

public interface IUsuarioService: IRepositoryBase<Usuario>
{
    string GenerateToken(Usuario usuario);
}