using System.Text.Json.Serialization;
using WebAppKey.Models.Enum;

namespace WebAppKey.DTO;

public class MovimentoDTO
{
    public DateTime DataInclusao { get; set; } = DateTime.Now;
    public int Numero { get; set; } 
    public eSituacaoMovimento Situacao { get; set; } = eSituacaoMovimento.smInclusao;
    public string Observacao { get; set; } = string.Empty;
    public int TipoMovimentoId { get; set; }

    public double Total { get; set; } = 0;   
    
    public ICollection<MovimentoItemDTO> Itens { get; set; } = new List<MovimentoItemDTO>();

    public MovimentoLavouraDTO MovimentoLavoura { get; set; } = new MovimentoLavouraDTO();

}