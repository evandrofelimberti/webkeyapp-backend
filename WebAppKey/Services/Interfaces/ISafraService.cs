using WebAppKey.DTO;
using WebAppKey.Models;

namespace WebAppKey.Services.Interfaces;

public interface ISafraService: IRepositoryBase<Safra>
{
    Task<Safra> createSafra(SafraDTO safraDto);
    Task<Safra> updateSafra(int Id, SafraDTO safraDto);
}