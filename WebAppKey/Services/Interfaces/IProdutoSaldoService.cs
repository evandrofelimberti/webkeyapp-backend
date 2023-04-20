using WebAppKey.Models;

namespace WebAppKey.Services.Interfaces;

public interface IProdutoSaldoService: IRepositoryBase<ProdutoSaldo>
{
     Task<bool> AtualizaProdutoSaldoFromMovimento(Movimento movimento);
}