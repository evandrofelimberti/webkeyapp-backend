using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using WebAppKey.Data;
using WebAppKey.DTO;
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
                
                var CustoEntrada = movimentoItens
                    .Where(m => m.ProdutoId == produtoId && m.Movimento.TipoMovimento.Tipo == eTipoMovimento.tmEntrada)
                    .OrderByDescending(item => item.Id).Select(v => v.Valor).FirstOrDefault(0.0);
                
                var CustoMedioEntrada = movimentoItens
                    .Where(m => m.ProdutoId == produtoId && m.Movimento.TipoMovimento.Tipo == eTipoMovimento.tmEntrada)
                    .Select(m => m.Valor).DefaultIfEmpty(0.0).Average();                   
                
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

                produtoSaldoAtual.ValorCompra = CustoEntrada;
                produtoSaldoAtual.ValorMedioCompra = CustoMedioEntrada;
                produtoSaldoAtual.ValorEntrada = saldoEntrada;
                produtoSaldoAtual.ValorSaida = saldoSaida;
                produtoSaldoAtual.ValorSaldo = (produtoSaldoAtual.ValorEntrada - produtoSaldoAtual.ValorSaida);

                if (existeProdutoSaldo)
                {
                    await Update(produtoSaldoAtual);
                }
                else
                {
                     await Add(produtoSaldoAtual);
                }
            }

            return true;
        }
        catch (Exception e)
        {
            throw new Exception($"Erro ao atualizar Produto Saldo! \n" +  e.Message);
        }
      
    }

    public async Task<bool> AtualizaProdutoSaldoFromProduto(ProdutoDTO produtoDto, int produtoId)
    {
        var produtoSaldo = await _context.ProdutoSaldo.Where(p => p.ProdutoId == produtoId)
            .FirstOrDefaultAsync();
        var existeProdutoSaldo = true;
        if (produtoSaldo == null)
        {
            existeProdutoSaldo = false;
            produtoSaldo = new ProdutoSaldo();
            produtoSaldo.ProdutoId = produtoId;
        }
        produtoSaldo.ValorVenda = produtoDto.ValorVenda;
        if (existeProdutoSaldo)
        {
            await Update(produtoSaldo);
        }
        else
        {
            await Add(produtoSaldo);
        }

        return true;
    }

    public async Task<bool> DeleteFromProdutoId(int produtoId)
    {
        var produtoSaldoId = await _context.ProdutoSaldo.Where(p => p.ProdutoId == produtoId).FirstAsync();
        if (produtoSaldoId != null)
        {
            await DeleteById(produtoSaldoId.Id);
        }

        return true;
    }
}