using WebAppKey.Data;
using WebAppKey.DTO;
using WebAppKey.Models;
using WebAppKey.Services.Interfaces;

namespace WebAppKey.Services;

public class SafraService: RepositoryBase<Safra>, ISafraService
{
    public SafraService(DataContext context): base(context)
    {
        
    }

    public async Task<Safra> createSafra(SafraDTO safraDto)
    {
        var safra = new Safra();
        safra.FromSafraDto(safraDto);
        await Add(safra);
        return safra;
    }

    public async Task<Safra> updateSafra(int Id, SafraDTO safraDto)
    {
        var safra = await GetById(Id);
        safra.FromSafraDto(safraDto);
        await Update(safra);
        return safra;
    }

}