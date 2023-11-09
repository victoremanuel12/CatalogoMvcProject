using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CategoriaMVC.Models
{
    public class ProductViewModel
    {
        [JsonPropertyName("produtoId")]
        [Required(ErrorMessage = "o nome do produto é obrigatrio")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        [Display(Name = "Nome")]

        [Required(ErrorMessage = "o nome do produto é obrigatrio")]
        public string Name { get; set; }

        [JsonPropertyName("descricao")]
        [Display(Name = "Descrição")]

        [Required(ErrorMessage = "a descrição do produto é obrigatrio")]
        public string Description { get; set; }

        [JsonPropertyName("preco")]
        [Display(Name = "Preço")]

        [Required(ErrorMessage = "o preço do produto é obrigatrio")]

        public decimal Price { get; set; }

        [JsonPropertyName("imagemUrl")]
        [Display(Name = "Imagem")]

        [Required(ErrorMessage = "a imagem do produto é obrigatrio")]

        public string? Image { get; set; }

        [JsonPropertyName("CategoriaId")]
        [Display(Name = "Categoria")]

        public int CategoriaId { get; set; }
    }
}
