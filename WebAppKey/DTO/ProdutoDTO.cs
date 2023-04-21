namespace WebAppKey.DTO;

public class ProdutoDTO
{
    public string Codigo { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int UnidadeId { get; set; }
    public int TipoProdutoId { get; set; }
    public double ValorVenda { get; set; } = 0.0;
}