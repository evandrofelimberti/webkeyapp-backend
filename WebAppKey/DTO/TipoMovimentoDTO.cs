using WebAppKey.Models.Enum;

namespace WebAppKey.DTO;

public class TipoMovimentoDTO
{
    public string Descricao { get; set; } = string.Empty;
    public eTipoMovimento Tipo { get; set; } = eTipoMovimento.tmNenhum;   
}