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
        private IValidator<CreateUnidadeInput> _validator;

        public UnidadeMutation(IUnidadeService unidadeService, IMapper mapper, IUnidadeValidator unidadeValidator, IValidator<CreateUnidadeInput> validator)
        {
            _unidadeService = unidadeService;
            _mapper = mapper;
            _unidadeValidator = unidadeValidator;
            _validator = validator;
        }

        public async Task<UnidadeType> CreateUnidade(CreateUnidadeInput input)
        {
            var validationResult = await _validator.ValidateAsync(input);
            if (!validationResult.IsValid)
            {
                throw new GraphQLException(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }            
            
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
    }
}