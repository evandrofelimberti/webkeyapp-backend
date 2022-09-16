using WebAppKey.DTO;
using WebAppKey.Models;

namespace WebAppKey.Services.Interfaces;

public interface IMovimentoService: IRepositoryBase<Movimento>
{
    Task<Movimento> CreateMovimento(MovimentoDTO movimentoDto);

    new Task<Movimento> GetById(int Id);
    
    Task<Movimento> UpdateMovimento(int Id, MovimentoDTO movimentoDto);    
}