using WebAppKey.DTO;
using WebAppKey.Models;

namespace WebAppKey.Services.Interfaces;

public interface IProdutoSaldoService: IRepositoryBase<ProdutoSaldo>
{
     Task<bool> AtualizaProdutoSaldoFromMovimento(Movimento movimento);
     Task<bool> AtualizaProdutoSaldoFromProduto(ProdutoDTO produtoDto, int produtoId);
     Task<bool> DeleteFromProdutoId(int produtoId);

}