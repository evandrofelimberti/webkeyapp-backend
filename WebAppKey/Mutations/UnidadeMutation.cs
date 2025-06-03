using AutoMapper;
using FluentValidation;
using WebAppKey.DTO;
using WebAppKey.Models;
using WebAppKey.Services.Interfaces;
using WebAppKey.Types;
using WebAppKey.Validators;
using WebAppKey.Validators.Intefaces;

namespace WebAppKey.Mutations
{
    public class UnidadeMutation
    {
        private readonly IUnidadeService _unidadeService;
        private readonly IMapper _mapper;
        private readonly IUnidadeValidator _unidadeValidator;

        public UnidadeMutation(IUnidadeService unidadeService, IMapper mapper, IUnidadeValidator unidadeValidator)
        {
            _unidadeService = unidadeService;
            _mapper = mapper;
            _unidadeValidator = unidadeValidator;
        }

        public async Task<UnidadeType> CreateUnidade(CreateUnidadeInput input)
        {
            await _unidadeValidator.ValidateCreateAsync(input);
            
            // var possuiSiglaCadastrada = await _unidadeValidator.PossuiSiglaCadastrada(input.Sigla);
            // if (possuiSiglaCadastrada)
            // {
            //     //throw new Exception("Sigla informada já possui cadastro!");
            //     throw new GraphQLException("Sigla informada já possui cadastro!");
            // }            
            
           var createDto = _mapper.Map<UnidadeDto>(input);
           await _unidadeService.CreateUnidade(createDto);
           return _mapper.Map<UnidadeType>(createDto);
        }

        public async Task<UnidadeType> UpdateUnidade(int id, UpdateUnidadeInput input)
        {
            await _unidadeValidator.ValidateUpdateAsync(input);

            var createDto = _mapper.Map<UnidadeDto>(input);
            await _unidadeService.UpdateUnidade(id, createDto);
            return _mapper.Map<UnidadeType>(createDto);            
        }
    }
}