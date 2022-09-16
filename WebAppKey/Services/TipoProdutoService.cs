using WebAppKey.Data;
using WebAppKey.Models;
using WebAppKey.Services.Interfaces;

namespace WebAppKey.Services;

public class TipoProdutoService: RepositoryBase<TipoProduto>, ITipoProdutoService
{
    public TipoProdutoService(DataContext context):base(context)
    {
        
    }
}