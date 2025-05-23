using WebAppKey.DTO;
using WebAppKey.Models;

namespace WebAppKey.Services.Interfaces;

public interface IUnidadeService: IRepositoryBase<Unidade>
{
    Task<Unidade> GetByFirstDescricao(string descricao);

    Task<UnidadeDto>  UpdateUnidade(int id, UnidadeDto unidadeDto);

}