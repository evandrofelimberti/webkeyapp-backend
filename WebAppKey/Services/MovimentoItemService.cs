using Microsoft.EntityFrameworkCore;
using WebAppKey.Data;
using WebAppKey.DTO;
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

    public async Task<Movimento> DeleteMovimentoItem(MovimentoDTO movimentoDto, int id)
    {
        var movimentoId = await _context.MovimentoItem.Where(m => m.Id == id).Select(m => m.MovimentoId).FirstOrDefaultAsync();
        await base.DeleteById(id);
        var movimentoService = new MovimentoService(_context);
        var movimento = await movimentoService.UpdateMovimento(movimentoId, movimentoDto);
        return movimento;
    }
}