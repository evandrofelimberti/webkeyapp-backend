namespace WebAppKey.DTO;
using WebAppKey.Models.Enum;

public class TipoProdutoDTO
{
    public string Descricao { get; set; } = string.Empty;
    public string Sigla { get; set; } = string.Empty;
    public eTipoProduto Tipo { get; set; } = eTipoProduto.tpNenhum;   
}