using Microsoft.EntityFrameworkCore;
using WebAppKey.Data;
using WebAppKey.DTO;
using WebAppKey.Models;
using WebAppKey.Services.Interfaces;

namespace WebAppKey.Services;

public class UnidadeService : RepositoryBase<Unidade>, IUnidadeService
{
    public UnidadeService(DataContext context) : base(context)
    {

    }

    public Task<Unidade> GetByFirstDescricao(string descricao)
    {
        return FirstOrDefault(u => u.Descricao == descricao);
    }
   
    public async Task<Unidade> UpdateUnidade(int id, UnidadeDto unidadeDto)
    {
        try
        {
            var unidade =  await GetById(id);
            
            unidade.Descricao = unidadeDto.Descricao;
            unidade.Sigla = unidadeDto.Sigla;
            await base.Update(unidade);
            return unidade;
        }
        catch (Exception exception) 
        {
            throw new Exception(exception.Message);
        }        
        
    }    
    
}