using System.Collections;
using Microsoft.AspNetCore.Mvc;
using WebAppKey.DTO;
using WebAppKey.Models;
using WebAppKey.Models.Enum;

namespace WebAppKey.Services.Interfaces;

public interface IMovimentoService: IRepositoryBase<Movimento>
{
    Task<Movimento> CreateMovimento(MovimentoDTO movimentoDto);

    new Task<Movimento> GetById(int Id);
    
    Task<Movimento> UpdateMovimento(int Id, MovimentoDTO movimentoDto);
    Task<ResultadoPaginacaoDTO<Movimento>> GetFiltroMovimento(FiltroPaginacaoDTO filtroPaginacaoDto);

    Task<ICollection<Movimento>> GetMovimentoLavouraSafra(int idSafra, int idLavoura, eTipoMovimento tipoMovimento);
    
    Task<bool> DeleteMovimento(int Id);
}