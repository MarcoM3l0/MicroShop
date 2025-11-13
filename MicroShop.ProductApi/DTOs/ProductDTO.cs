using MicroShop.ProductApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MicroShop.ProductApi.DTOs;

public class ProductDTO
{
    public int ProductId { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [MinLength(3, ErrorMessage = "O campo Nome deve ter pelo menos 3 caracteres.")]
    [MaxLength(100, ErrorMessage = "O campo Nome deve ter no máximo 100 caracteres.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "O campo Preço é obrigatório.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
    [MinLength(5, ErrorMessage = "O campo Descrição deve ter pelo menos 5 caracteres.")]
    [MaxLength(200, ErrorMessage = "O campo Descrição deve ter no máximo 200 caracteres.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "O campo Estoque é obrigatório.")]
    [Range(1, 9999)]
    public long Stock { get; set; }
    public string? ImageURL { get; set; }

    public string? CategoryName { get; set; }

    [JsonIgnore]
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}
