using WebAppKey.DTO;
using WebAppKey.Models;

namespace WebAppKey.Services.Interfaces;

public interface IUnidadeService: IRepositoryBase<Unidade>
{
    Task<Unidade> GetByFirstSigla(string sigla);

    Task<UnidadeDto>  UpdateUnidade(int id, UnidadeDto unidadeDto);
    
    Task<UnidadeDto>  CreateUnidade(UnidadeDto unidadeDto);

    
}