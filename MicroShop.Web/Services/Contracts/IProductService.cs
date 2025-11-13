using MicroShop.Web.Models;

namespace MicroShop.Web.Services.Contracts;

public interface IProductService
{
    Task<IEnumerable<ProductViewModel>> GetAllProducts();
    Task<ProductViewModel> GetProductById(int productId);
    Task<ProductViewModel> CreateProduct(ProductViewModel productVM);
    Task<ProductViewModel> UpdateProduct(ProductViewModel productVM);
    Task<bool> DeleteProduct(int productId);
}
