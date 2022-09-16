using WebAppKey.Models;

namespace WebAppKey.Services.Interfaces;

public interface IMovimentoItemService:IRepositoryBase<MovimentoItem>
{
    Task<ICollection<MovimentoItem>> GetAllMovimentoId(int id);
}