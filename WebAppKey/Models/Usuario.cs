using System.ComponentModel.DataAnnotations;
using WebAppKey.Models.Enum;

namespace WebAppKey.Models;

public class Usuario
{
    //[Required]
    //[Key]
    public int Id { get; set; }

    [Display(Name = "Nome")]
    [Required(ErrorMessage = "{0} obrigatório")]
    [MaxLength(100, ErrorMessage ="{0} tem mais que 100 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Display(Name = "Senha")]
    [Required(ErrorMessage = "{0} obrigatório")]
    [MaxLength(100, ErrorMessage = "{0} tem mais que 100 caracteres")]
    public string Senha { get; set; } = string.Empty;

    [Display(Name = "Tipo")]
    [Required(ErrorMessage = "{0} obrigatório")]
    public eTipoUsuario Tipo { get; set; } = eTipoUsuario.tuNenhum;           
}