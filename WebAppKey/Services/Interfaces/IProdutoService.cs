using Microsoft.AspNetCore.Mvc;
using WebAppKey.DTO;

namespace WebAppKey.Services.Interfaces;
using WebAppKey.Models;

public interface IProdutoService: IRepositoryBase<Produto>
{
    Task<Produto> UpdateProduto(int Id, ProdutoDTO produtoDto);
    Task<Produto>  CreateProduto(ProdutoDTO produtoDto);
    new Task<Produto> GetById(int Id);

}