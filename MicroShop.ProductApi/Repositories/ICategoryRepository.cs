using MicroShop.ProductApi.Models;

namespace MicroShop.ProductApi.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAll();
    Task<IEnumerable<Category>> GetCategoriesProducts();
    Task<Category?> GetById(int id);
    Task<Category> Create(Category category);
    Task<Category?> Update(int id, Category category);
    Task<Category> Delete(int id);
}
