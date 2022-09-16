using WebAppKey.DTO;
using WebAppKey.Models;

namespace WebAppKey.Services.Interfaces;

public interface IUnidadeService: IRepositoryBase<Unidade>
{
    Task<Unidade> GetByFirstDescricao(string descricao);

    Task<Unidade>  UpdateUnidade(int id, UnidadeDto unidadeDto);

}