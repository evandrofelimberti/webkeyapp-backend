using WebAppKey.DTO;
using WebAppKey.Models;

namespace WebAppKey.Services.Interfaces;

public interface IUnidadeService: IRepositoryBase<Unidade>
{
    Task<bool> GetByFirstSiglaAsync(string sigla, int? ignoreId = null);

    Task<UnidadeDto>  UpdateUnidade(int id, UnidadeDto unidadeDto);
    
    Task<UnidadeDto>  CreateUnidade(UnidadeDto unidadeDto);

    
}