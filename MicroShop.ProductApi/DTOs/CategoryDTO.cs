using MicroShop.ProductApi.Models;
using System.ComponentModel.DataAnnotations;

namespace MicroShop.ProductApi.DTOs;

public class CategoryDTO
{
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [MinLength(3, ErrorMessage = "O campo Nome deve ter pelo menos 3 caracteres.")]
    [MaxLength(100, ErrorMessage = "O campo Nome deve ter no máximo 100 caracteres.")]
    public string? Name { get; set; }
    public ICollection<Product>? Products { get; set; }
}
