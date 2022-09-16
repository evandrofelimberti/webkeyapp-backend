using WebAppKey.Data;
using WebAppKey.Models;
using WebAppKey.Services.Interfaces;

namespace WebAppKey.Services;

public class TipoMovimentoService: RepositoryBase<TipoMovimento>, ITipoMovimentoService
{
    public TipoMovimentoService(DataContext context):base(context)
    {
        
    }
}