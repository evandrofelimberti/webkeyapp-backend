using Microsoft.EntityFrameworkCore;
using WebAppKey.Data;
using WebAppKey.Models;
using WebAppKey.Services.Interfaces;

namespace WebAppKey.Services;

public class MovimentoItemService: RepositoryBase<MovimentoItem>, IMovimentoItemService
{
    public MovimentoItemService(DataContext context) : base(context)
    {
        
    }

    public async Task<ICollection<MovimentoItem>> GetAllMovimentoId(int id)
    {
        var itens = await _context.MovimentoItem
            .Include(p => p.Produto)
            .Include(m => m.Movimento)
            .Where(m => m.MovimentoId == id).ToListAsync();
        return itens;
    }
}