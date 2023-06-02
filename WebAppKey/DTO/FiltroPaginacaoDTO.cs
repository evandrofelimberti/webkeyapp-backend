using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace WebAppKey.DTO;

public class FiltroPaginacaoDTO
{
    public string Nome { get; set; } = string.Empty;
    public int NumeroPagina { get; set; }
    public int TamanhoPagina { get; set; }
    
    public FiltroPaginacaoDTO()
    {
        this.NumeroPagina = 1;
        this.TamanhoPagina = 10;
    }
    public FiltroPaginacaoDTO(int numeroPagina, int tamanhoPagina, string nome)
    {
        this.Nome = nome;
        this.NumeroPagina = numeroPagina;
        this.TamanhoPagina = tamanhoPagina;
        // this.NumeroPagina = numeroPagina < 1 ? 1 : numeroPagina;
        // this.TamanhoPagina = tamanhoPagina > 10 ? 10 : tamanhoPagina;
    }
}