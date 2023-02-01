using WebAppKey.Models.Enum;

namespace WebAppKey.DTO;

public class UsuarioDTO
{
    public string Nome { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public eTipoUsuario Tipo { get; set; } = eTipoUsuario.tuNenhum;   
}