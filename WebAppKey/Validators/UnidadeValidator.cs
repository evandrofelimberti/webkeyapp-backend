using WebAppKey.DTO;
using WebAppKey.Services;
using WebAppKey.Services.Interfaces;
using WebAppKey.Validators.Intefaces;

namespace WebAppKey.Validators
{
    public class UnidadeValidator: IUnidadeValidator
    {
        private readonly IUnidadeService _unidadeService;
        public UnidadeValidator(IUnidadeService unidadeService)
        {
            _unidadeService = unidadeService;
        }
        
        public async Task<bool> PossuiSiglaCadastrada(string sigla)
        {
            var unidade = await _unidadeService.GetByFirstSigla(sigla);
            return unidade != null;
        }
    }
}