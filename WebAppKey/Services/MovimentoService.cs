using Microsoft.EntityFrameworkCore;
using WebAppKey.Data;
using WebAppKey.DTO;
using WebAppKey.Models;
using WebAppKey.Services.Interfaces;

namespace WebAppKey.Services;

public class MovimentoService: RepositoryBase<Movimento>, IMovimentoService
{
    public MovimentoService(DataContext context) : base(context)
    {
                
    }
    new public async Task<Movimento> GetById(int Id)
    {
        var movimento = await _context.Movimento
            .Where(m => m.Id == Id)
            .Include(m => m.Itens)
            .Include(tm => tm.TipoMovimento)
            .FirstOrDefaultAsync();
        if (movimento == null)
        {
            throw new Exception($"Identificador {Id} não encontrado!");
        }
        
        return movimento;
    }

    new public async Task<IEnumerable<Movimento>> GetAll()
    {
        var movimentos = await _context.Movimento
            .Include(m => m.Itens)
            .Include(tm => tm.TipoMovimento)
            .ToListAsync();
        return movimentos;
    }

    public async Task<Movimento> CreateMovimento(MovimentoDTO movimentoDto)
    {
        var tipomovimento =  await (new TipoMovimentoService(_context).GetById(movimentoDto.TipoMovimentoId));
        var movimento = new Movimento();
        movimento.FromMovimentoDTO(movimentoDto);
        movimento.TipoMovimento = tipomovimento;
        
        await Add(movimento);
        return movimento;
    }

    public async Task<Movimento> UpdateMovimento(int Id, MovimentoDTO movimentoDto)
    {
        try
        {
            var tipomovimento =  await (new TipoMovimentoService(_context).GetById(movimentoDto.TipoMovimentoId));
            var movimento = await GetById(Id);
            movimento.FromMovimentoDTO(movimentoDto);
            movimento.TipoMovimento = tipomovimento;
        
            await Update(movimento);
            return movimento;
        }
        catch(Exception ex)
        {
            throw new Exception($"Erro ao Atualizar Movimento {Id}. \n" +  ex.Message);
        }

    }      
}