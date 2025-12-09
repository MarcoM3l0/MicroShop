using System.ComponentModel.DataAnnotations;

namespace MicroShop.Web.Models;

public class ProductViewModel
{
    public int ProductId { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public string? Description { get; set; }
    [Required]
    public int Stock { get; set; }
    [Required]
    public string? ImageUrl { get; set; }
    public string? CategoryName { get; set; }
    [Display(Name = "Categoria")]
    public int CategoryId { get; set; }
}
