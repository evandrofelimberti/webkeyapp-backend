using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAppKey.Data;
using WebAppKey.DTO;
using WebAppKey.Models;
using WebAppKey.Services.Interfaces;

namespace WebAppKey.Services;

public class UnidadeService : RepositoryBase<Unidade>, IUnidadeService
{
    public UnidadeService(DataContext context, IMapper mapper) : base(context, mapper)
    {

    }

    public Task<Unidade> GetByFirstDescricao(string descricao)
    {
        return FirstOrDefault(u => u.Descricao == descricao);
    }
   
    public async Task<UnidadeDto> UpdateUnidade(int id, UnidadeDto unidadeDto)
    {
        try
        {
            var unidade =  await GetById(id);
            
            unidade.Descricao = unidadeDto.Descricao;
            unidade.Sigla = unidadeDto.Sigla;
            // copia os valores de unidadeDto para unidade 
            _mapper.Map(unidadeDto, unidade);

            await base.Update(unidade);            
            // cria uma nova instancia 
            return _mapper.Map<UnidadeDto>(unidade);

        }
        catch (Exception exception) 
        {
            throw new Exception(exception.Message);
        }        
        
    }    
    
}