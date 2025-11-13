using System.Text.Json.Serialization;

namespace MicroShop.ProductApi.Models;

public class Product
{
    public int ProductId { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public long Stock { get; set; }
    public string? ImageURL { get; set; }
    [JsonIgnore]
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}
