namespace WebAppKey.DTO;

public class SafraDTO
{
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);
    public DateTime DataFim { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);    

}