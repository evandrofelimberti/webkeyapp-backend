using FluentValidation;
using WebAppKey.DTO;
using WebAppKey.Services;
using WebAppKey.Services.Interfaces;
using WebAppKey.Types;
using WebAppKey.Validators.Intefaces;

namespace WebAppKey.Validators
{
    public class UnidadeValidator: IUnidadeValidator
    {
        private readonly IUnidadeService _unidadeService;
        private readonly IValidator<CreateUnidadeInput> _createValidator;
        private readonly IValidator<UpdateUnidadeInput> _updateValidator;

        public UnidadeValidator(IUnidadeService unidadeService,
            IValidator<CreateUnidadeInput> createValidator,
            IValidator<UpdateUnidadeInput> updateValidator)
        {
            _unidadeService = unidadeService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        
        public async Task<bool> PossuiSiglaCadastrada(string sigla)
        {
            var unidade = await _unidadeService.GetByFirstSiglaAsync(sigla);
            return unidade != null;
        }

        public async Task ValidateCreateAsync(CreateUnidadeInput input)
        {        
            var result = await _createValidator.ValidateAsync(input);
            if (!result.IsValid)
                RetornaMensagemValidation(result);
        }

        public async Task ValidateUpdateAsync(UpdateUnidadeInput input)
        {
            var result = await _updateValidator.ValidateAsync(input);
            if (!result.IsValid)
                RetornaMensagemValidation(result);
        }
        
        private static void RetornaMensagemValidation(FluentValidation.Results.ValidationResult result)
        {
            throw new GraphQLException(string.Join("; ", result.Errors.Select(e => e.ErrorMessage)));            
        }        
    }
}