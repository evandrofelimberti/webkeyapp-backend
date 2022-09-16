using System.ComponentModel.DataAnnotations;
using WebAppKey.DTO;
using WebAppKey.Models.Enum;
namespace WebAppKey.Models;

public class TipoMovimento
{
    [Required]
    [Key]
    public int Id { get; set; }

    [Display(Name = "Descrição")]
    [Required(ErrorMessage = "{0} obrigatório")]
    [MaxLength(250, ErrorMessage ="{0} tem mais que 250 caracteres")]
    public string Descricao { get; set; } = string.Empty;

    [Display(Name = "Tipo")]
    [Required(ErrorMessage = "{0} obrigatório")]
    public eTipoMovimento Tipo { get; set; } = eTipoMovimento.tmNenhum;

    public TipoMovimento()
    {
        
    }

    public void FromTipoMovimentoDto(TipoMovimentoDTO tipoMovimentoDto)
    {
        this.Descricao = tipoMovimentoDto.Descricao;
        this.Tipo = tipoMovimentoDto.Tipo;
    }
    
}