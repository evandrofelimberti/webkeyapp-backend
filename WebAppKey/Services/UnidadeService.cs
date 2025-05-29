using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAppKey.Data;
using WebAppKey.DTO;
using WebAppKey.Models;
using WebAppKey.Services.Interfaces;
using WebAppKey.Validators.Intefaces;

namespace WebAppKey.Services;

public class UnidadeService : RepositoryBase<Unidade>, IUnidadeService
{
    public UnidadeService(DataContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public Task<Unidade> GetByFirstSigla(string sigla)
    {
        return FirstOrDefault(u => u.Sigla == sigla);
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

    public async Task<UnidadeDto> CreateUnidade(UnidadeDto unidadeDto)
    {
        var unidade = _mapper.Map<Unidade>(unidadeDto);
        await Add(unidade);
        return  _mapper.Map<UnidadeDto>(unidade);;        
    }
    
}