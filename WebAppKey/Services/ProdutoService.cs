using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WebAppKey.DTO;
using WebAppKey.Models;
using WebAppKey.Services.Interfaces;

namespace WebAppKey.Services;
using Data;

public class ProdutoService: RepositoryBase<Produto>, IProdutoService
{

    public ProdutoService(DataContext context):base(context)
    {
        
    }
    
    new public async Task<Produto> GetById(int Id)
    {
        var produto = await _context.Produto
            .Include(u => u.Unidade)
            .Include(t => t.TipoProduto)
            .Include(s => s.ProdutoSaldo)
            .Where(p => p.Id == Id)
            .FirstOrDefaultAsync();
        if (produto == null)
        {
            throw new Exception($"Identificador {Id} não encontrado!");
        }
        return produto;
    }    
    
    new public async Task<IEnumerable<Produto>> GetAll()
    {
        //Using Method Syntax
       /* var produto = _context.Produto
            .GroupJoin(
                _context.ProdutoSaldo,
                p => p.Id,
                s => s.ProdutoId,
                (p, s) => new { p, s }
            )
            .SelectMany(
                x => x.s.DefaultIfEmpty(),
                (produto, saldo) => new
                {
                    Saldo = saldo == null ? 0.0 : saldo.ValorSaldo
                }
            );*/
        
        var produtos = await _context.Produto
            .Include(u => u.Unidade)
            .Include(t => t.TipoProduto)
            .Include(s => s.ProdutoSaldo)
            .OrderBy(p => p.Nome)
            .ToListAsync();
       
        return produtos;
    }
    
    public async Task<ResultadoPaginacaoDTO<Produto>> GetAllPagination(FiltroPaginacaoDTO filtrodto)
    {
        var resultPaginacao = new ResultadoPaginacaoDTO<Produto>();
        
        var filtro = new FiltroPaginacaoDTO(filtrodto.NumeroPagina, filtrodto.TamanhoPagina, filtrodto.Nome);
        
        var produtos = await _context.Produto
            .Include(u => u.Unidade)
            .Include(t => t.TipoProduto)
            .Include(s => s.ProdutoSaldo)
            .Where(p => p.Nome.ToLower().Contains(filtro.Nome.ToLower()))
            .OrderBy(p => p.Nome)
            .ToListAsync();

        resultPaginacao.DataSemPaginacao = produtos;
        resultPaginacao.filtro = filtro;

        resultPaginacao.CalcularPaginacao();
        
        return resultPaginacao;
    }
    
    public async Task<int> CountAsync()
    {
        return await _context.Produto.CountAsync();
    }     
    
    public async Task<Produto> UpdateProduto(int Id, ProdutoDTO produtoDto)
    {
        try
        {
            var produto = await GetById(Id);
            produto.FromProdutoDto(produtoDto);
            await base.Update(produto);
            var produtoSaldoService = new ProdutoSaldoService(_context);
            await produtoSaldoService.AtualizaProdutoSaldoFromProduto(produtoDto, Id);
            return produto;
        }
        catch(Exception ex)
        {
            throw new Exception($"Erro ao Atualizar Produto {Id}. \n" +  ex.Message);
        }
    }

    public async Task<Produto> CreateProduto(ProdutoDTO produtoDto)
    {
        try
        {
            var unidade = await (new UnidadeService(_context).GetById(produtoDto.UnidadeId));
            var tipoProduto = await(new TipoProdutoService(_context).GetById(produtoDto.TipoProdutoId));

            var newProduto = new Produto();
            newProduto.FromProdutoDto(produtoDto);
            newProduto.Unidade = unidade;
            newProduto.TipoProduto = tipoProduto;

            await base.Add(newProduto);
            var produtoSaldoService = new ProdutoSaldoService(_context);
            await produtoSaldoService.AtualizaProdutoSaldoFromProduto(produtoDto, newProduto.Id);            
            return await  GetById(newProduto.Id);
        }
        catch (Exception e)
        {
            throw new Exception($"Erro ao Salvar Produto {produtoDto.Descricao}. \n" + e.Message);
        }
    }

    public async Task<bool> DeleteProduto(int Id)
    {
        try
        {
             bool possuiProduto = _context.MovimentoItem.Where(i => i.Produto.Id == Id).Any();
            if (possuiProduto)
            {
                throw new Exception($"Produto possui movimentação! ");
            }

            await base.DeleteById(Id);
            var produtoSaldoService = new ProdutoSaldoService(_context);
            await produtoSaldoService.DeleteFromProdutoId(Id);  
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }    
    
}