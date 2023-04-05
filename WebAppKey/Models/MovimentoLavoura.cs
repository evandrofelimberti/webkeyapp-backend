using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using WebAppKey.DTO;

namespace WebAppKey.Models
{
    public class MovimentoLavoura
    {
        [Key]
        [Required]        
        public int Id { get; set; }
        [Required]
        public int MovimentoId { get; set; }
        [JsonIgnore]
        public Movimento Movimento { get; set; }

        [Required]
        public int LavouraId { get; set; }
        public Lavoura Lavoura { get; set; }
        
        [Required]
        public int SafraId { get; set; }
        public Safra Safra { get; set; }        

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data Realizado")]
        [Required(ErrorMessage = "{0} obrigatório")]
        public DateTime DataRealizado { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);

        [Display(Name = "Observação")]
        [MaxLength(250, ErrorMessage = "Campo {0} tem mais que 500 caracteres")]
        public string Observacao { get; set; } = string.Empty;

        public void FromMovimentoLavouraDTO(MovimentoLavouraDTO movimentoLavouraDto)
        {
            if (movimentoLavouraDto.LavouraId > 0)
            {
                this.LavouraId = movimentoLavouraDto.LavouraId;
                this.Observacao = movimentoLavouraDto.Observacao;
                this.DataRealizado = movimentoLavouraDto.DataRealizado;
                this.SafraId = movimentoLavouraDto.SafraId;
            }

        }
        
    }
}
