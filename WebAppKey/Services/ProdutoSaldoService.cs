using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using WebAppKey.Data;
using WebAppKey.Models;
using WebAppKey.Models.Enum;
using WebAppKey.Services.Interfaces;

namespace WebAppKey.Services;

public class ProdutoSaldoService: RepositoryBase<ProdutoSaldo>, IProdutoSaldoService
{
    public ProdutoSaldoService(DataContext context):base(context)
    {
       
    }

    public async Task<bool> AtualizaProdutoSaldoFromMovimento(Movimento movimento)
    {
        try
        {
            var produtoIds = movimento.Itens.Select(m => m.ProdutoId).Distinct().ToList();
            var movimentoItens = await _context.MovimentoItem
                .Include(m => m.Movimento)
                .Include(tm => tm.Movimento.TipoMovimento)
                .Where(p => produtoIds.Contains(p.ProdutoId))
                .ToListAsync();
            
            List<int> produtoSaldoInserido = new List<int>();
           
            var existeProdutoSaldo = true;

            foreach (var produtoId in produtoIds)
            {
                if (produtoSaldoInserido.Contains(produtoId))
                {
                   continue; 
                }
                produtoSaldoInserido.Add(produtoId);

                var saldoEntrada = movimentoItens
                    .Where(m => m.ProdutoId == produtoId && m.Movimento.TipoMovimento.Tipo == eTipoMovimento.tmEntrada)
                    .Select(m => m.Quantidade).DefaultIfEmpty(0.0).Sum();
                
                var saldoSaida = movimentoItens
                    .Where(m => m.ProdutoId == produtoId && m.Movimento.TipoMovimento.Tipo == eTipoMovimento.tmSaida)
                    .Select(m => m.Quantidade).DefaultIfEmpty(0.0).Sum();                
                
                var produtoSaldoAtual = await _context.ProdutoSaldo.Where(p => p.ProdutoId == produtoId)
                    .FirstOrDefaultAsync();

                if (produtoSaldoAtual == null)
                {
                    existeProdutoSaldo = false;
                    produtoSaldoAtual = new ProdutoSaldo();
                    produtoSaldoAtual.ProdutoId = produtoId;
                }
                
                produtoSaldoAtual.ValorEntrada = saldoEntrada;
                produtoSaldoAtual.ValorSaida = saldoSaida;
                produtoSaldoAtual.ValorSaldo = (produtoSaldoAtual.ValorEntrada - produtoSaldoAtual.ValorSaida);

                if (existeProdutoSaldo)
                {
                    await base.Update(produtoSaldoAtual);
                }
                else
                {
                     await base.Add(produtoSaldoAtual);
                }
            }

            return true;
        }
        catch (Exception e)
        {
            throw new Exception($"Erro ao atualizar Produto Saldo! \n" +  e.Message);
        }
      
    } 
}