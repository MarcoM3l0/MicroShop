using MicroShop.ProductApi.DTOs;

namespace MicroShop.ProductApi.Services.interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDTO>> GetProducts();
    Task<ProductDTO?> GetProductById(int id);
    Task AddProduct(ProductDTO productDto);
    Task UpdateProduct(ProductDTO productDto);
    Task DeleteProduct(int id);
}
