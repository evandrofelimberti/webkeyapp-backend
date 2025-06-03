using WebAppKey.Types;

namespace WebAppKey.Validators.Intefaces
{
    public interface IUnidadeValidator
    {
        Task<bool> PossuiSiglaCadastrada(string sigla);
        Task ValidateCreateAsync(CreateUnidadeInput input);
        Task ValidateUpdateAsync(UpdateUnidadeInput input);        
    }
}