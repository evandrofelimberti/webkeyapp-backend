using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using WebAppKey.DTO;
using WebAppKey.Models.Enum;

namespace WebAppKey.Models
{
    public class Produto
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Código")]
        [MaxLength(20, ErrorMessage = "Campo {0} tem mais que 20 caracteres")]
        public string Codigo { get; set; } = string.Empty;

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0} obrigatório")]
        [MaxLength(300, ErrorMessage = "Campo {0} tem mais que 300 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Display(Name = "Descrição")]
        [MaxLength(500, ErrorMessage = "Campo {0} tem mais que 500 caracteres")]
        public string Descricao { get; set; } = string.Empty;

        [Display(Name = "Unidade")]
        [Required(ErrorMessage = "Campo {0} é obrigatório")]
        public int UnidadeId { get; set; }

        [Display(Name = "Tipo Produto")]
        [Required(ErrorMessage = "Campo {0} é obrigatório")]
        public int TipoProdutoId { get; set; } 
        
        //[JsonIgnore]
        public TipoProduto TipoProduto { get; set; } 
        
        //[JsonIgnore]
        public Unidade Unidade { get; set; }

        public Produto()
        {
            
        }
        
        public void FromProdutoDto (ProdutoDTO produtoDto)
        {
            this.Nome = produtoDto.Nome;
            this.Codigo = produtoDto.Codigo;
            this.Descricao = produtoDto.Descricao;
            this.UnidadeId = produtoDto.UnidadeId;
            this.TipoProdutoId = produtoDto.TipoProdutoId;
        }

    }
}
