using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebAppKey.DTO
{
    public class LavouraDTO
    {
        public string Descricao { get; set; } = string.Empty;
        public double AreaHa { get; set; } = 0;
    }
}
