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
    }
}

public class ResultadoPaginacaoDTO<T>{
    public int TotalPaginas { get;  private set; }
    public ICollection<T> Data { get; private set; }
    public FiltroPaginacaoDTO filtro { get; set; }
    public ICollection<T> DataSemPaginacao { get; set; }

    
    public void CalcularPaginacao()
    {
        var countMovimentos = DataSemPaginacao.Count;
        var totalPaginas =  ((double)countMovimentos / (double)filtro.TamanhoPagina);
        int roundtotalPaginas = Convert.ToInt32(Math.Ceiling(totalPaginas));

        this.TotalPaginas = roundtotalPaginas;
        this.Data = DataSemPaginacao.Skip((filtro.NumeroPagina) * filtro.TamanhoPagina)
            .Take(filtro.TamanhoPagina).ToList();            
    }
}