using WebAppKey.DTO;
using WebAppKey.Models;

namespace WebAppKey.Services.Interfaces;

public interface ILavouraService: IRepositoryBase<Lavoura>
{
    Task<Lavoura> createLavoura(LavouraDTO lavouraDto);
    Task<Lavoura> updateLavoura(int Id, LavouraDTO lavouraDto);
}
