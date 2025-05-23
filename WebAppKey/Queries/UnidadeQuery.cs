using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAppKey.Models;
using WebAppKey.Services.Interfaces;
using WebAppKey.Types;

namespace WebAppKey.Queries
{
    public class UnidadeQuery
    {
        private readonly IUnidadeService  _unidadeService;
        private readonly IMapper _mapper;

        public UnidadeQuery(IUnidadeService unidadeService, IMapper mapper)
        {
            _unidadeService = unidadeService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UnidadeType>> GetUnidades()
        {
            var unidades = await _unidadeService.GetAll();
            return _mapper.Map<IEnumerable<UnidadeType>>(unidades);
        }

        public async Task<UnidadeType> GetUnidade(int id)
        {
            var unidade = await _unidadeService.GetById(id);
            return unidade == null ? null : _mapper.Map<UnidadeType>(unidade);
        }
    }
}