using System.ComponentModel.DataAnnotations;

namespace WebAppKey.Models
{
    public class Lavoura
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "{0} obrigatório")]
        public string Descricao { get; set; } = string.Empty;

        [Display(Name = "Área Ha")]
        [Required(ErrorMessage = "{0} obrigatório")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double AreaHa { get; set; } = 0;

    }
}
