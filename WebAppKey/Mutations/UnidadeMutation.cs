using AutoMapper;
using WebAppKey.DTO;
using WebAppKey.Models;
using WebAppKey.Services.Interfaces;
using WebAppKey.Types;

namespace WebAppKey.Mutations
{
    public class UnidadeMutation
    {
        private readonly IUnidadeService _unidadeService;
        private readonly IMapper _mapper;

        public UnidadeMutation(IUnidadeService unidadeService, IMapper mapper)
        {
            _unidadeService = unidadeService;
            _mapper = mapper;
        }

        public async Task<UnidadeType> CreateUnidade(CreateUnidadeInput input)
        {
            var createDto = _mapper.Map<Unidade>(input);
           await _unidadeService.Add(createDto);
            return _mapper.Map<UnidadeType>(createDto);
        }
    }
}