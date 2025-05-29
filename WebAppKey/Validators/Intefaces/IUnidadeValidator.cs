namespace WebAppKey.Validators.Intefaces
{
    public interface IUnidadeValidator
    {
        Task<bool> PossuiSiglaCadastrada(string sigla); 
    }
}