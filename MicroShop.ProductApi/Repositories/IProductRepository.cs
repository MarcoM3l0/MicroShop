using MicroShop.ProductApi.Models;

namespace MicroShop.ProductApi.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product?> GetById(int id);
    Task<Product> Create(Product product);
    Task<Product?> Update(int id, Product product);
    Task<Product> Delete(int id);
}
