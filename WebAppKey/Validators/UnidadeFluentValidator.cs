using System.Data;
using FluentValidation;
using WebAppKey.DTO;
using WebAppKey.Services.Interfaces;
using WebAppKey.Types;

namespace WebAppKey.Validators
{
    public class UnidadeFluentValidator: AbstractValidator<CreateUnidadeInput>
    {
        private readonly IUnidadeService _unidadeService;

        public UnidadeFluentValidator(IUnidadeService unidadeService)
        {
            _unidadeService = unidadeService;
            RuleFor(u => u.Sigla)
                .NotEmpty()
                .WithMessage("Sigla obrigatória")
                .Length(2,3)
                .WithMessage("Deve conter entre dois a tres caracteres")
                .MustAsync(async (sigla, ct) => !await SiglaValidaAsync(sigla, null, ct))                
                .WithMessage("Já existe uma unidade com essa sigla");
            
            RuleFor(x => x.Descricao)
                .NotEmpty()
                .WithMessage("Descrição é obrigatória")
                .MaximumLength(300)
                .WithMessage("Descrição deve ter no máximo 300 caracteres");            
        }

        private async Task<bool> SiglaValidaAsync(string sigla, int? ignoreId = null, CancellationToken cancellationToken = default)
        {
            return !await _unidadeService.GetByFirstSiglaAsync(sigla, ignoreId);
            
        }
    }
    
    public class UpdateUnidadeFluentValidator: AbstractValidator<UpdateUnidadeInput>
    {
        private readonly IUnidadeService _unidadeService;

        public UpdateUnidadeFluentValidator(IUnidadeService unidadeService)
        {
            _unidadeService = unidadeService;
            RuleFor(u => u.Sigla)
                .NotEmpty()
                .WithMessage("Sigla obrigatória")
                .Length(2,3)
                .WithMessage("Deve conter entre dois a tres caracteres")
                .When(x => x.Sigla != null) // só valida se estiver sendo alterada
                .MustAsync(async (input, sigla, ct) =>
                    !await SiglaValida(sigla!, input.Id, ct))                
                .WithMessage("Já existe uma unidade com essa sigla");
            
            RuleFor(x => x.Descricao)
                .NotEmpty()
                .WithMessage("Descrição é obrigatória")
                .MaximumLength(300)
                .WithMessage("Descrição deve ter no máximo 300 caracteres");            
        }

        private async Task<bool> SiglaValida(string sigla, int? ignoreId = null, CancellationToken cancellationToken = default)
        {
            return !await _unidadeService.GetByFirstSiglaAsync(sigla, ignoreId);
            
        }
    }    
}