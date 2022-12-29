using System.ComponentModel.DataAnnotations;
using WebAppKey.DTO;

namespace WebAppKey.Models;

public class Safra
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Display(Name = "Descrição")]
    [Required(ErrorMessage = "{0} obrigatório")]
    public string Descricao { get; set; } = string.Empty;
    
    [Display(Name = "Data Início")]
    [Required(ErrorMessage = "{0} obrigatório")]
    public DateTime DataInicio { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);

    [Display(Name = "Data Fim")]
    [Required(ErrorMessage = "{0} obrigatório")]
    public DateTime DataFim { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);    
    
    public void FromSafraDto(SafraDTO safraDto)
    {
        this.Descricao = safraDto.Descricao;
        this.DataInicio = safraDto.DataInicio;
        this.DataFim = safraDto.DataFim;
    }
}