using MicroShop.Web.Models;

namespace MicroShop.Web.Services.Contracts;

public interface IProductService
{
    Task<IEnumerable<ProductViewModel>> GetAllProducts(string token);
    Task<ProductViewModel> GetProductById(int productId, string token);
    Task<ProductViewModel> CreateProduct(ProductViewModel productVM, string token);
    Task<ProductViewModel> UpdateProduct(ProductViewModel productVM, string token);
    Task<bool> DeleteProduct(int productId, string token);
}
