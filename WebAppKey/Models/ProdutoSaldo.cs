using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebAppKey.DTO;

namespace WebAppKey.Models;

[Index(nameof(ProdutoId), IsUnique = true)]
public class ProdutoSaldo
{
    [Required]
    [Key]
    public int Id { get; set; }

    [JsonIgnore]
    public Produto Produto { get; set;}
    
    [Display(Name = "ProdutoId")]
    public int ProdutoId { get; set; }

    [Required(ErrorMessage = "{0} obrigatório")]
    [Range(Double.MaxValue, double.MaxValue, ErrorMessage = "{0} must be from {1} to {2}")]
    [Display(Name = "Valor Saldo")]
    [DisplayFormat(DataFormatString = "{0:F2}")]
    public double ValorSaldo { get; set; } = 0;

    [Required(ErrorMessage = "{0} obrigatório")]
    [Range(Double.MaxValue, double.MaxValue, ErrorMessage = "{0} must be from {1} to {2}")]
    [Display(Name = "Valor Entrada")]
    [DisplayFormat(DataFormatString = "{0:F2}")]
    public double ValorEntrada { get; set; } = 0;

    [Required(ErrorMessage = "{0} obrigatório")]
    [Range(Double.MaxValue, double.MaxValue, ErrorMessage = "{0} must be from {1} to {2}")]
    [Display(Name = "Saldo Saida")]
    [DisplayFormat(DataFormatString = "{0:F2}")]
    public double ValorSaida { get; set; } = 0;
    
    [Required(ErrorMessage = "{0} obrigatório")]
    [Range(Double.MaxValue, double.MaxValue, ErrorMessage = "{0} must be from {1} to {2}")]
    [Display(Name = "Valor Compra")]
    [DisplayFormat(DataFormatString = "{0:F2}")]
    public double ValorCompra { get; set; } = 0;    
    
    [Required(ErrorMessage = "{0} obrigatório")]
    [Range(Double.MaxValue, double.MaxValue, ErrorMessage = "{0} must be from {1} to {2}")]
    [Display(Name = "Valor Medio Compra")]
    [DisplayFormat(DataFormatString = "{0:F2}")]
    public double ValorMedioCompra { get; set; } = 0;

    [Required(ErrorMessage = "{0} obrigatório")]
    [Range(Double.MaxValue, double.MaxValue, ErrorMessage = "{0} must be from {1} to {2}")]
    [Display(Name = "Valor Venda")]
    [DisplayFormat(DataFormatString = "{0:F2}")]
    public double ValorVenda { get; set; } = 0;        
    
    public ProdutoSaldo()
    {
            
    }
    
}