using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CategoriaMVC.Models
{
    public class CategoryViewModel
    {
        [JsonPropertyName("categoriaId")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        [Required(ErrorMessage = "Informe o nome da categoria")]
        [MinLength(3)]
        [MaxLength(100)]
        public string? Name { get; set; }

        [JsonPropertyName("imagemUrl")]
        [Required(ErrorMessage = "Informe a Url da imagem da categoria")]
        public string? ImageUrl { get; set; }
        
    }
}
