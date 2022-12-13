using Microsoft.AspNetCore.Mvc;
using WebAppKey.Data;
using WebAppKey.DTO;
using WebAppKey.Models;
using WebAppKey.Services.Interfaces;

namespace WebAppKey.Services;

public class LavouraService: RepositoryBase<Lavoura>, ILavouraService
{
    public LavouraService(DataContext context): base(context)
    {
        
    }

    public async Task<Lavoura> createLavoura(LavouraDTO lavouraDto)
    {
        var lavoura = new Lavoura();
        lavoura.FromLavoutaDto(lavouraDto);
        await Add(lavoura);
        return lavoura;
    }

    public async Task<Lavoura> updateLavoura(int Id, LavouraDTO lavouraDto)
    {
        var lavoura = await GetById(Id);
        lavoura.FromLavoutaDto(lavouraDto);
        await Update(lavoura);
        return lavoura;
    }

}