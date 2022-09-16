using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    
    public async Task<Produto> UpdateProduto(int Id, ProdutoDTO produtoDto)
    {
        try
        {
            var produto = await GetById(Id);
            produto.FromProdutoDto(produtoDto);
            await base.Update(produto);
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
            return await  GetById(newProduto.Id);
        }
        catch (Exception e)
        {
            throw new Exception($"Erro ao Salvar Produto {produtoDto.Descricao}. \n" + e.Message);
        }

    }

    /*  public async Task<ActionResult<List<Produto>>> FindAll()
    {
        return await _context.Produtos.ToListAsync();
    }

    public async Task<IEnumerable<Produto>> ListAll()
    {
        return await _context.Produtos.ToListAsync();
    }

    public async Task<Produto> GetById(int Id)
    {
        return await _context.Produtos
            .Where(p => p.Id == Id)
            .Include(p => p.Unidade)
            .Include(t => t.TipoProduto).FirstOrDefaultAsync();
    }*/
}