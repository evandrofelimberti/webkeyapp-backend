namespace WebAppKey.Types
{
    public class UnidadeType
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string Sigla { get; set; } = string.Empty;
    }
    
    public class CreateUnidadeInput
    {
        public string Descricao { get; set; } = string.Empty;
        public string Sigla { get; set; } = string.Empty;
    }

    public class UpdateUnidadeInput
    {
        public string Descricao { get; set; } = string.Empty;
        public string Sigla { get; set; } = string.Empty;
    }    
}