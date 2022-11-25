using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;
using WebAppKey.Data;
using WebAppKey.DTO;
using WebAppKey.Models.Enum;
using WebAppKey.Services;

namespace WebAppKey.Models;

public class MovimentoItem
{
    [Required]
    [Key]
    public int Id { get; set; }
    
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]    
    [Display(Name = "Data Inclusão")]
    [Required(ErrorMessage = "{0} obrigatório")]
    public DateTime DataInclusao { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);
    
    [Required(ErrorMessage = "{0} obrigatório")]    
    [Display(Name = "Descrição")]
    [MaxLength(250, ErrorMessage = "Campo {0} tem mais que 250 caracteres")]
    public string Descricao { get; set; } = string.Empty;
    
    [Display(Name = "Produto")]
    [Required(ErrorMessage = "{0} obrigatório")]
    public int ProdutoId { get; set; }
    [JsonIgnore]
    public Produto Produto { get; set; }
    
    [Display(Name = "Movimento")]
    [Required(ErrorMessage = "{0} obrigatório")]  
    public int MovimentoId { get; set; }
    [JsonIgnore] 
    public Movimento Movimento { get; set; }    
    
    
    [Required(ErrorMessage = "{0} obrigatório")]
    [Range(Double.MaxValue, double.MaxValue, ErrorMessage ="{0} must be from {1} to {2}")]
    [Display(Name = "Quantidade")]
    [DisplayFormat(DataFormatString = "{0:F2}")]
    public double Quantidade { get; set; }  
    
    [Required(ErrorMessage = "{0} obrigatório")]
    [Range(Double.MaxValue, double.MaxValue, ErrorMessage ="{0} must be from {1} to {2}")]
    [Display(Name = "Valor")]
    [DisplayFormat(DataFormatString = "{0:F2}")]
    public double Valor { get; set; }    
    
    [Required(ErrorMessage = "{0} obrigatório")]
    [Range(Double.MaxValue, double.MaxValue, ErrorMessage ="{0} must be from {1} to {2}")]
    [Display(Name = "Total")]
    [DisplayFormat(DataFormatString = "{0:F2}")]
    public double Total { get; set; }

    public MovimentoItem()
    {
        
    }

    public void FromMovimentoItemDTO(MovimentoItemDTO itemDto)
    {
        this.Id = itemDto.Id;
        //this.DataInclusao = itemDto.DataInclusao;
        this.Descricao = itemDto.Descricao;
        this.ProdutoId = itemDto.ProdutoId;
        this.MovimentoId = itemDto.MovimentoId;
        this.Quantidade = itemDto.Quantidade;
        this.Valor = itemDto.Valor;
        this.Total = itemDto.Total;
    }
    
}