namespace WebAppKey.DTO;

public class MovimentoItemDTO
{
    public DateTime DataInclusao { get; set; } = DateTime.Now;
    public string Descricao { get; set; } = string.Empty;
    public int ProdutoId { get; set; }
    public int MovimentoId { get; set; }
    public double Quantidade { get; set; }    
    public double Valor { get; set; }    
    public double Total { get; set; }   
}