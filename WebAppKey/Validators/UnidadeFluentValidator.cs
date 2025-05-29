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
                .MustAsync(SiglaValida) // deve retornar sempre false para levantar a exceção
                .WithMessage("Já existe uma unidade com essa sigla");
            
            RuleFor(x => x.Descricao)
                .NotEmpty()
                .WithMessage("Descrição é obrigatória")
                .MaximumLength(300)
                .WithMessage("Descrição deve ter no máximo 300 caracteres");            
        }

        private async Task<bool> SiglaValida(string sigla, CancellationToken cancellationToken)
        {
            var unidade = await _unidadeService.GetByFirstSigla(sigla);
            return unidade == null;
        }
    }
}