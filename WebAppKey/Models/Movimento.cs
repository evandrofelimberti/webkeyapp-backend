using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using WebAppKey.Data;
using WebAppKey.DTO;
using WebAppKey.Models.Enum;
namespace WebAppKey.Models;

public class Movimento
{
    [Required]
    [Key]
    public int Id { get; set; }
    
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]       
    [Display(Name = "Data Inclusão")]
    [Required(ErrorMessage = "{0} obrigatório")]
    public DateTime DataInclusao { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);
    
    [Display(Name = "Numero")]
    [Required(ErrorMessage = "{0} obrigatório")]
    public int Numero { get; set; }
    
    [Display(Name = "Situação")]
    [Required(ErrorMessage = "{0} obrigatório")]
    public eSituacaoMovimento Situacao { get; set; } = eSituacaoMovimento.smInclusao;

    [Display(Name = "Observação")]
    [MaxLength(250, ErrorMessage = "Campo {0} tem mais que 500 caracteres")]
    public string Observacao { get; set; } = string.Empty;
    
    [Display(Name = "Tipo Movimento")]
    [Required(ErrorMessage = "Campo {0} é obrigatório")]
    public int TipoMovimentoId { get; set; }
    
    [JsonIgnore]
    public TipoMovimento TipoMovimento { get; set; }
    //  [JsonIgnore]
    public ICollection<MovimentoItem> Itens { get; set; } = new List<MovimentoItem>();

    [Display(Name = "Total")]
    [DisplayFormat(DataFormatString = "{0:F2}")]
    public double Total { get; set; } = 0;

    public Movimento()
    {
        
    }

    public void FromMovimentoDTO(MovimentoDTO movimentoDto)
    {
        this.Numero = movimentoDto.Numero;
        this.Situacao = movimentoDto.Situacao;
        this.DataInclusao = movimentoDto.DataInclusao;
        this.TipoMovimentoId = movimentoDto.TipoMovimentoId;
        this.Observacao = movimentoDto.Observacao;
       
        foreach (var itemDto in movimentoDto.Itens)
        {   
            if (itemDto.Id == 0)
            {
                var newItem = new MovimentoItem();
                newItem.FromMovimentoItemDTO(itemDto);                
                this.AddItem(newItem);
            }
            else
            {
                var itemUpdate = this.Itens.Where(item => item.Id == itemDto.Id).First();
                if (itemUpdate != null)
                {
                    itemUpdate.FromMovimentoItemDTO(itemDto);
                }
            }
        }
        this.Total = this.TotalItens();
    }
    
    public void AddItem(MovimentoItem item)
    {
        Itens.Add(item);
    }  
    public void RemoveItem(MovimentoItem item)
    {
        Itens.Remove(item);
    }

    public double TotalItens()
    {
        return Itens.Sum(m => m.Total);
    }
    
}