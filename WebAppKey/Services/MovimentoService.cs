using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppKey.Data;
using WebAppKey.DTO;
using WebAppKey.Models;
using WebAppKey.Models.Enum;
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
            .Include(ml => ml.MovimentoLavoura)
            .ThenInclude(l => l.Lavoura)
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
            .Include(ml => ml.MovimentoLavoura)
            .ThenInclude(l => l.Lavoura).OrderByDescending(o => o.Id)
            .ToListAsync();
        return movimentos;
    }

    public async Task<Movimento> CreateMovimento(MovimentoDTO movimentoDto)
    {
        var tipomovimento =  await (new TipoMovimentoService(_context).GetById(movimentoDto.TipoMovimentoId));
        var movimento = new Movimento();
        movimento.FromMovimentoDTO(movimentoDto);
        movimento.TipoMovimento = tipomovimento;
        if (movimentoDto.MovimentoLavoura.LavouraId > 0)
        {
            var lavoura = await (new LavouraService(_context).GetById(movimentoDto.MovimentoLavoura.LavouraId));
            movimento.MovimentoLavoura.Lavoura = lavoura;
        }
       
        await Add(movimento);
        var produtoSaldo = new ProdutoSaldoService(_context);
        await produtoSaldo.AtualizaProdutoSaldoFromMovimento(movimento);
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
            if (movimentoDto.MovimentoLavoura.LavouraId > 0)
            {
                var lavoura = await (new LavouraService(_context).GetById(movimentoDto.MovimentoLavoura.LavouraId));
                movimento.MovimentoLavoura.Lavoura = lavoura;
            }
            
            await Update(movimento);
            var produtoSaldo = new ProdutoSaldoService(_context);
            await produtoSaldo.AtualizaProdutoSaldoFromMovimento(movimento);            
            return movimento;
        }
        catch(Exception ex)
        {
            throw new Exception($"Erro ao Atualizar Movimento {Id}. \n" +  ex.Message);
        }
    }

    public async Task<ICollection<Movimento>> GetMovimentoLavouraSafra(int idSafra, int idLavoura, eTipoMovimento tipoMovimento)
    {
        var despesas = await _context.Movimento
            .Include(i => i.Itens)
            .Include(ml => ml.MovimentoLavoura)
            .Include(l => l.MovimentoLavoura.Lavoura)
            .Where(m => m.MovimentoLavoura.Safra.Id == idSafra &&
                        m.MovimentoLavoura.Lavoura.Id == idLavoura &&
                        m.TipoMovimento.Tipo == tipoMovimento).ToListAsync();
        return despesas;
    }

    public async Task<bool> DeleteMovimento(int Id)
    {
        var movimento = _context.Movimento
            .Where(m => m.Id == Id)
            .Include(m => m.Itens)
            .Include(tm => tm.TipoMovimento)
            .FirstOrDefault();        
        
        await base.DeleteById(Id);
        if (movimento != null)
        {
            var produtoSaldo = new ProdutoSaldoService(_context);
            await produtoSaldo.AtualizaProdutoSaldoFromMovimento(movimento);            
        }

        
        return true;
    }
}