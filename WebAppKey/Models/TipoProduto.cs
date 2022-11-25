using WebAppKey.DTO;

namespace WebAppKey.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebAppKey.Models.Enum;

public class TipoProduto
{
    [Required]
    [Key]
    public int Id { get; set; }

    [Display(Name = "Descrição")]
    [Required(ErrorMessage = "{0} obrigatório")]
    [MaxLength(300, ErrorMessage ="{0} tem mais que 300 caracteres")]
    public string Descricao { get; set; } = string.Empty;

    [Display(Name = "Sigla")]
    [Required(ErrorMessage = "{0} obrigatório")]
    [MaxLength(3, ErrorMessage = "{0} tem mais que 3 caracteres")]
    public string Sigla { get; set; } = string.Empty;

    [Display(Name = "Tipo")]
    [Required(ErrorMessage = "{0} obrigatório")]
    public eTipoProduto Tipo { get; set; } = eTipoProduto.tpNenhum;    
    
   // [JsonIgnore]
   // public List<Produto> produtos { get; set; }

    public TipoProduto()
    {
        
    }

    public void FromTipoProdutoDto(TipoProdutoDTO tipoProdutoDto)
    {
        this.Descricao = tipoProdutoDto.Descricao;
        this.Sigla = tipoProdutoDto.Sigla;
        this.Tipo = tipoProdutoDto.Tipo;
    }
}