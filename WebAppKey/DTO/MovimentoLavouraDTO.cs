using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using WebAppKey.Models;

namespace WebAppKey.DTO
{
    public class MovimentoLavouraDTO
    {
        public int MovimentoId { get; set; }
        public int LavouraId { get; set; }
        public int SafraId { get; set; }
        public DateTime DataRealizado { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);
        public string Observacao { get; set; } = string.Empty;
    }
}
